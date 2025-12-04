using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.AdditionalContent.Tag;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.DTO.Streetcode.Types;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteFull;
using Streetcode.BLL.Util;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Enums;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Realizations.Base;
using static System.Net.Mime.MediaTypeNames;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create
{
    public class CreateStreetcodeHandler : IRequestHandler<CreateStreetcodeCommand, Result<JsonElement>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IMediator _mediator;
        private readonly StreetcodeCreateHelper _streetcodeCreateHelper;

        public CreateStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _streetcodeCreateHelper = new StreetcodeCreateHelper(_logger);
        }

        public async Task<Result<JsonElement>> Handle(CreateStreetcodeCommand request, CancellationToken cancellationToken)
        {
            // try catch block is temporary solution until validation would be implemented.
            try
            {
                var rawJson = request.rawJsonCreateDTO;

                int streetcodeIndex = rawJson.GetProperty("Index").GetInt32();

                if (await StreetcodeIndexExists(streetcodeIndex))
                {
                    return Result.Fail(new Error($"Streetcode with Index {streetcodeIndex} already exists"));
                }

                string streetcodeType = rawJson.GetProperty("StreetcodeType").GetString();

                CreateStreetcodeDto сreateStreetcodeDTO = _streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

                var streetcodeContent = _mapper.Map<StreetcodeContent>(сreateStreetcodeDTO);

                await _repository.StreetcodeRepository.CreateAsync(streetcodeContent);
                await _repository.SaveChangesAsync();

                var audioResult = await HandleAudioCreate(сreateStreetcodeDTO, streetcodeContent, request);
                if (audioResult.IsFailed)
                {
                    return audioResult;
                }

                var imagesResult = await HandleImagesCreate(сreateStreetcodeDTO, streetcodeContent, request);
                if (imagesResult.IsFailed)
                {
                    return imagesResult;
                }

                var tagsResult = await HandleTagsCreate(сreateStreetcodeDTO, streetcodeContent, request);
                if (tagsResult.IsFailed)
                {
                    return tagsResult;
                }

                var resultIsSuccess = await _repository.SaveChangesAsync() > 0;

                if (resultIsSuccess)
                {
                    var streetcodeDTO = _mapper.Map<CreateStreetcodeDto>(streetcodeContent);
                    var jsonResult = JsonSerializer.SerializeToElement(streetcodeDTO);
                    return await Task.FromResult(Result.Ok(jsonResult));
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Exception occurred while creating streetcode: {ex.Message}";
                _logger.LogError(request, errorMsg);

                _mediator.Send(new DeleteFullStreetcodeCommand(
                    request.rawJsonCreateDTO.GetProperty("Index").GetInt32()));

                return Result.Fail<JsonElement>(new Error(errorMsg));
            }

            return await Task.FromResult(Result.Ok());
        }

        private async Task<bool> StreetcodeIndexExists(int index)
        {
            var existing = await _repository.StreetcodeRepository.GetAllAsync(sc => sc.Index == index);
            return existing.Any();
        }

        private async Task<Result> HandleAudioCreate(CreateStreetcodeDto dto, StreetcodeContent entity, CreateStreetcodeCommand request)
        {
            if (dto.AudioId is null)
            {
                return Result.Ok();
            }

            var audio = await _repository.AudioRepository.GetFirstOrDefaultAsync(x => x.Id == dto.AudioId);
            if (audio is null)
            {
                _logger.LogError(request, "Audio not found");
                return Result.Fail("Audio not found");
            }

            entity.AudioId = audio.Id;
            return Result.Ok();
        }

        private async Task<Result> HandleImagesCreate(CreateStreetcodeDto dto, StreetcodeContent entity, CreateStreetcodeCommand request)
        {
            if (dto.Images is null)
            {
                return Result.Ok();
            }

            foreach (var img in dto.Images)
            {
                var image = await _repository.ImageRepository.GetFirstOrDefaultAsync(x => x.Id == img.ImageId);
                if (image is null)
                {
                    string errorMsg = $"Image {img.ImageId} not found";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                await _repository.StreetcodeImageRepository.CreateAsync(new StreetcodeImage
                {
                    ImageId = img.ImageId,
                    StreetcodeId = entity.Id
                });

                var imgDetail = _mapper.Map<ImageDetails>(img);
                await _repository.ImageDetailsRepository.CreateAsync(imgDetail);
            }

            return Result.Ok();
        }

        private async Task<Result> HandleTagsCreate(CreateStreetcodeDto dto, StreetcodeContent entity, CreateStreetcodeCommand request)
        {
            if (dto.Tags is null)
            {
                return Result.Ok();
            }

            var tagList = dto.Tags.ToList();
            foreach (var tag in tagList)
            {
                var thisTag = await _repository.TagRepository.GetFirstOrDefaultAsync(x => x.Id == tag.Id);
                if (thisTag is null)
                {
                    string errorMsg = $"Tag {tag.Id} not found";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                await _repository.StreetcodeTagIndexRepository.CreateAsync(new StreetcodeTagIndex
                {
                    StreetcodeId = entity.Id,
                    TagId = tag.Id,
                    IsVisible = tag.IsVisible,
                    Index = tagList.IndexOf(tag)
                });
            }

            return Result.Ok();
        }
    }
}
