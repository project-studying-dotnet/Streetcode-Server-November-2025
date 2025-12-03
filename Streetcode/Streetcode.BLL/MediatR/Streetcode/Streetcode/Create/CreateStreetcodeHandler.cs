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
        private StreetcodeCreateHelper _streetcodeCreateHelper;

        public CreateStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _streetcodeCreateHelper = new StreetcodeCreateHelper(_logger);
        }

        public async Task<Result<JsonElement>> Handle(CreateStreetcodeCommand request, CancellationToken cancellationToken)
        {
            // try catch block is temporary solution until validation would be implemented.
            try
            {
                var rawJson = request.rawJsonCreateDTO;

                var streetcodeIndex = rawJson.GetProperty("Index").GetInt32();
                var existingStreetcodeWithIndex = await _repository.StreetcodeRepository
                    .GetAllAsync(sc => sc.Index == streetcodeIndex);
                if (existingStreetcodeWithIndex.Any())
                {
                    throw new Exception();
                }

                var streetcodeType = rawJson.GetProperty("StreetcodeType").GetString();

                CreateStreetcodeDto сreateStreetcodeDTO;

                сreateStreetcodeDTO = _streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

                var streetcodeContent = _mapper.Map<StreetcodeContent>(сreateStreetcodeDTO);

                var result = _repository.StreetcodeRepository.Create(streetcodeContent);
                await _repository.SaveChangesAsync();

                var audio = await _repository.AudioRepository.GetFirstOrDefaultAsync(
                    x => x.Id == сreateStreetcodeDTO.AudioId);

                if (audio == null && сreateStreetcodeDTO.AudioId != null)
                {
                    const string errorMsg = "Streetcode not found";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(new Error(errorMsg));
                }

                if (сreateStreetcodeDTO.Images != null)
                {
                    foreach (var img in сreateStreetcodeDTO.Images)
                    {
                        var image = await _repository.ImageRepository
                            .GetFirstOrDefaultAsync(x => x.Id == img.ImageId);

                        await _repository.StreetcodeImageRepository.CreateAsync(new StreetcodeImage()
                        {
                            ImageId = img.ImageId,
                            StreetcodeId = streetcodeContent.Id
                        });

                        var imgDetail = _mapper.Map<ImageDetails>(img);
                        await _repository.ImageDetailsRepository.CreateAsync(imgDetail);
                    }
                }

                List<StreetcodeTagDto> tagsList = сreateStreetcodeDTO.Tags.ToList();

                if (tagsList != null)
                {
                    foreach (var tag in tagsList)
                    {
                        var thisTag = await _repository.TagRepository
                            .GetFirstOrDefaultAsync(x => x.Id == tag.Id);

                        StreetcodeTagIndex tagIndex = new StreetcodeTagIndex()
                        {
                            StreetcodeId = streetcodeContent.Id,
                            TagId = tag.Id,
                            IsVisible = tag.IsVisible,
                            Index = tagsList.IndexOf(tag),
                        };

                        _repository.StreetcodeTagIndexRepository.Create(tagIndex);
                    }
                }

                var resultIsSuccess = await _repository.SaveChangesAsync() > 0;

                if (resultIsSuccess)
                {
                    var streetcodeDTO = _mapper.Map<CreateStreetcodeDto>(result);
                    var jsonResult = JsonSerializer.SerializeToElement(streetcodeDTO);
                    return await Task.FromResult(Result.Ok(jsonResult));
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Exception occurred while creating streetcode: {ex.Message}";
                _logger.LogError(request, errorMsg);
                return Result.Fail<JsonElement>(new Error(errorMsg));
            }

            return await Task.FromResult(Result.Ok());
        }
    }
}
