using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Streetcode.BLL.DTO.AdditionalContent.Tag;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.DTO.Streetcode.Types;
using Streetcode.BLL.Interfaces.Cache;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
using Streetcode.BLL.Util;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Update
{
    public class UpdateStreetcodeHandler : IRequestHandler<UpdateStreetcodeCommand, Result<JsonElement>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly ICacheService _cacheService;
        private readonly StreetcodeCreateHelper _streetcodeCreateHelper;
        public UpdateStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _cacheService = cacheService;
            _streetcodeCreateHelper = new StreetcodeCreateHelper(_logger);
        }

        public async Task<Result<JsonElement>> Handle(UpdateStreetcodeCommand request, CancellationToken cancellationToken)
        {
            // try catch block is temporary solution until validation would be implemented.
            try
            {
                var rawJson = request.rawJsonUpdateDTO;

                var streetcodeType = rawJson.GetProperty("StreetcodeType").GetString();

                UpdateStreetcodeDto updateStreetcodeDTO = _streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

                var existingStreetcode = await GetExistingStreetcode(updateStreetcodeDTO.Id, request);
                if (existingStreetcode is null)
                {
                    return Result.Fail(new Error("Streetcode not found"));
                }

                if (!TryMapStreetcode(updateStreetcodeDTO, existingStreetcode))
                {
                    return Result.Fail<JsonElement>(new Error("StreetcodeType value can't be changed"));
                }

                var audioResult = await HandleAudioUpdate(updateStreetcodeDTO, existingStreetcode, request);
                if (audioResult.IsFailed)
                {
                    return audioResult;
                }

                var imagesResult = await HandleImagesUpdate(updateStreetcodeDTO, existingStreetcode, request);
                if (imagesResult.IsFailed)
                {
                    return imagesResult;
                }

                var tagsResult = await HandleTagsUpdate(updateStreetcodeDTO, existingStreetcode, request);
                if (tagsResult.IsFailed)
                {
                    return tagsResult;
                }

                _repository.StreetcodeRepository.Update(existingStreetcode);

                var resultIsSuccess = await _repository.SaveChangesAsync() > 0;

                if (resultIsSuccess)
                {
                    await _cacheService.RemoveAsync($"Streetcode_{updateStreetcodeDTO.Id}");

                    var streetcodeDTO = _mapper.Map<UpdateStreetcodeDto>(existingStreetcode);
                    var jsonResult = JsonSerializer.SerializeToElement(streetcodeDTO);
                    return Result.Ok(jsonResult);
                }
            }
            catch(Exception ex)
            {
                string errorMsg = $"Exception occurred while updating streetcode: {ex.Message}";
                _logger.LogError(request, errorMsg);
                return Result.Fail<JsonElement>(new Error(errorMsg));
            }

            return await Task.FromResult(Result.Ok());
        }

        private async Task<StreetcodeContent> GetExistingStreetcode(int id, UpdateStreetcodeCommand request)
        {
            var sc = await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(s => s.Id == id);
            if (sc is null)
            {
                _logger.LogError(request, "Streetcode not found");
            }

            return sc;
        }

        private bool TryMapStreetcode(UpdateStreetcodeDto dto, StreetcodeContent entity)
        {
            try
            {
                _mapper.Map(dto, entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<Result> HandleAudioUpdate(UpdateStreetcodeDto dto, StreetcodeContent entity, UpdateStreetcodeCommand request)
        {
            if (dto.AudioId is null)
            {
                entity.AudioId = null;
                return Result.Ok();
            }

            if (dto.AudioId < 0)
            {
                _logger.LogError(request, "invalid audio Id");
                return Result.Fail("invalid audio Id");
            }

            var audio = await _repository.AudioRepository.GetFirstOrDefaultAsync(a => a.Id == dto.AudioId);
            if (audio is null)
            {
                _logger.LogError(request, "Audio doesn't exist");
                return Result.Fail("Audio doesn't exist");
            }

            entity.AudioId = audio.Id;
            return Result.Ok();
        }

        private async Task<Result> HandleImagesUpdate(UpdateStreetcodeDto dto, StreetcodeContent entity, UpdateStreetcodeCommand request)
        {
            if (dto.Images is null)
            {
                return Result.Ok();
            }

            var streetcodeImages = (await _repository.StreetcodeImageRepository.GetAllAsync(i => i.StreetcodeId == entity.Id)).ToList();
            var imgIds = streetcodeImages.Select(i => i.ImageId).ToList();
            var imageDetails = (await _repository.ImageDetailsRepository.GetAllAsync(id => imgIds.Contains(id.ImageId))).ToList();

            _repository.StreetcodeImageRepository.DeleteRange(streetcodeImages);
            _repository.ImageDetailsRepository.DeleteRange(imageDetails);

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
                    StreetcodeId = dto.Id
                });

                var imgDetail = _mapper.Map<ImageDetails>(img);
                await _repository.ImageDetailsRepository.CreateAsync(imgDetail);
            }

            return Result.Ok();
        }

        private async Task<Result> HandleTagsUpdate(UpdateStreetcodeDto dto, StreetcodeContent entity, UpdateStreetcodeCommand request)
        {
            if (dto.Tags is null)
            {
                return Result.Ok();
            }

            var oldTags = (await _repository.StreetcodeTagIndexRepository.GetAllAsync(t => t.StreetcodeId == entity.Id)).ToList();
            _repository.StreetcodeTagIndexRepository.DeleteRange(oldTags);

            var newTagList = dto.Tags.ToList();
            foreach (var tag in newTagList)
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
                    StreetcodeId = dto.Id,
                    TagId = tag.Id,
                    IsVisible = tag.IsVisible,
                    Index = newTagList.IndexOf(tag)
                });
            }

            return Result.Ok();
        }
    }
}
