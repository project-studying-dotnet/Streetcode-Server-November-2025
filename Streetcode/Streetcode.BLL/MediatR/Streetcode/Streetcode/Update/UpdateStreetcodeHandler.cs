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
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
using Streetcode.BLL.Util;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Repositories.Realizations.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Update
{
    public class UpdateStreetcodeHandler : IRequestHandler<UpdateStreetcodeCommand, Result<JsonElement>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private StreetcodeCreateHelper _streetcodeCreateHelper;
        public UpdateStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
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

                var existingStreetcode = await _repository.StreetcodeRepository
                    .GetFirstOrDefaultAsync(sc => sc.Id == updateStreetcodeDTO.Id);

                if (existingStreetcode == null)
                {
                    const string errorMsg = "Streetcode not found";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(new Error(errorMsg));
                }

                try
                {
                    _mapper.Map(updateStreetcodeDTO, existingStreetcode);
                }
                catch (Exception)
                {
                    return Result.Fail("StreetcodeType value can't be changed");
                }

                switch (updateStreetcodeDTO.AudioId)
                {
                    case < 0:
                        const string errorMsgNegative = "invalid audio Id";
                        _logger.LogError(request, errorMsgNegative);
                        return Result.Fail(new Error(errorMsgNegative));
                    case null:
                        existingStreetcode.AudioId = null;
                        break;
                    default:
                        var audio = await _repository.AudioRepository.GetFirstOrDefaultAsync(a => a.Id == updateStreetcodeDTO.AudioId);
                        if (audio == null)
                        {
                            const string errorMsg = "Audio doesn't exist";
                            _logger.LogError(request, errorMsg);
                            return Result.Fail(new Error(errorMsg));
                        }
                        else
                        {
                            existingStreetcode.AudioId = audio?.Id;
                        }

                        break;
                }

                if (updateStreetcodeDTO.Images != null)
                {
                    var oldImages = existingStreetcode.Images.ToList();

                    var streetcodeImages = (await _repository.StreetcodeImageRepository
                    .GetAllAsync(i => i.StreetcodeId == existingStreetcode.Id)).ToList();

                    var imgIds = streetcodeImages.Select(i => i.ImageId).ToList();

                    var imageDtails = (await _repository.ImageDetailsRepository
                        .GetAllAsync(id => imgIds.Contains(id.ImageId))).ToList();

                    _repository.StreetcodeImageRepository.DeleteRange(streetcodeImages);
                    _repository.ImageDetailsRepository.DeleteRange(imageDtails);

                    foreach (var img in updateStreetcodeDTO.Images)
                    {
                        var image = await _repository.ImageRepository
                            .GetFirstOrDefaultAsync(x => x.Id == img.ImageId);

                        if (image == null)
                        {
                            string errorMsg = $"Image {img.ImageId} not found";
                            _logger.LogError(request, errorMsg);
                            return Result.Fail(new Error(errorMsg));
                        }

                        await _repository.StreetcodeImageRepository.CreateAsync(
                            new StreetcodeImage
                            {
                                ImageId = img.ImageId,
                                StreetcodeId = updateStreetcodeDTO.Id
                            });

                        var imgDetail = _mapper.Map<ImageDetails>(img);
                        await _repository.ImageDetailsRepository.CreateAsync(imgDetail);
                    }
                }

                if (updateStreetcodeDTO.Tags != null)
                {
                    var oldTags = (await _repository.StreetcodeTagIndexRepository
                    .GetAllAsync(t => t.StreetcodeId == existingStreetcode.Id)).ToList();

                    _repository.StreetcodeTagIndexRepository.DeleteRange(oldTags);

                    List<StreetcodeTagDto> newTagList = updateStreetcodeDTO.Tags.ToList();

                    foreach (var tag in newTagList)
                    {
                        var dbTag = await _repository.TagRepository
                            .GetFirstOrDefaultAsync(x => x.Id == tag.Id);

                        if (dbTag == null)
                        {
                            string errorMsg = $"Tag {tag.Id} not found";
                            _logger.LogError(request, errorMsg);
                            return Result.Fail(new Error(errorMsg));
                        }

                        StreetcodeTagIndex tagIndex = new StreetcodeTagIndex
                        {
                            StreetcodeId = updateStreetcodeDTO.Id,
                            TagId = tag.Id,
                            IsVisible = tag.IsVisible,
                            Index = newTagList.IndexOf(tag),
                        };

                        _repository.StreetcodeTagIndexRepository.Create(tagIndex);
                    }
                }

                _repository.StreetcodeRepository.Update(existingStreetcode);

                var resultIsSuccess = await _repository.SaveChangesAsync() > 0;

                if (resultIsSuccess)
                {
                    var streetcodeDTO = _mapper.Map<UpdateStreetcodeDto>(existingStreetcode);
                    var jsonResult = JsonSerializer.SerializeToElement(streetcodeDTO);
                    return Result.Ok(jsonResult);
                }
                else
                {
                    const string errorMsg = "Failed to update streetcode";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail<JsonElement>(new Error(errorMsg));
                }
            }
            catch(Exception ex)
            {
                string errorMsg = $"Exception occurred while updating streetcode: {ex.Message}";
                _logger.LogError(request, errorMsg);
                return Result.Fail<JsonElement>(new Error(errorMsg));
            }
        }
    }
}
