using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.AdditionalContent.Tag;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.DTO.Streetcode.Types;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
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

        public UpdateStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<JsonElement>> Handle(UpdateStreetcodeCommand request, CancellationToken cancellationToken)
        {
            var rawJson = request.rawJsonUpdateDTO;

            var streetcodeType = rawJson.GetProperty("StreetcodeType").GetString();

            UpdateStreetcodeDto updateStreetcodeDTO = ChoseStreetcodeType(streetcodeType, request);

            var existingStreetcode = await _repository.StreetcodeRepository
                .GetFirstOrDefaultAsync(sc => sc.Id == updateStreetcodeDTO.Id);

            if (existingStreetcode == null)
            {
                const string errorMsg = "Streetcode not found";
                _logger.LogError(request, errorMsg);
                return Result.Fail<JsonElement>(new Error(errorMsg));
            }

            _mapper.Map(updateStreetcodeDTO, existingStreetcode);

            if (updateStreetcodeDTO.AudioId < 0)
            {
                existingStreetcode.AudioId = null;
            }
            else
            {
                var audio = await _repository.AudioRepository.GetFirstOrDefaultAsync(a => a.Id == updateStreetcodeDTO.AudioId);
                if (audio == null)
                {
                    _logger.LogError(request, "Audio not found");
                }
                else
                {
                    existingStreetcode.AudioId = audio?.Id;
                }
            }

            if (updateStreetcodeDTO.Images != null)
            {
                var oldImages = existingStreetcode.Images.ToList();

                var streetcodeImages = _repository.StreetcodeImageRepository
                .GetAllAsync(i => i.StreetcodeId == existingStreetcode.Id).Result.ToList();

                var imgIds = streetcodeImages.Select(i => i.ImageId).ToList();
                var imageDtails = _repository.ImageDetailsRepository
                    .GetAllAsync(id => imgIds.Contains(id.ImageId)).Result.ToList();

                _repository.StreetcodeImageRepository.DeleteRange(streetcodeImages);
                _repository.ImageDetailsRepository.DeleteRange(imageDtails);

                foreach (var img in updateStreetcodeDTO.Images)
                {
                    var image = await _repository.ImageRepository
                        .GetFirstOrDefaultAsync(x => x.Id == img.ImageId);

                    if (image == null)
                    {
                        _logger.LogError(request, $"Image {img.ImageId} not found");
                        continue;
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
                var oldTags = _repository.StreetcodeTagIndexRepository
                .GetAllAsync(t => t.StreetcodeId == existingStreetcode.Id).Result.ToList();

                _repository.StreetcodeTagIndexRepository.DeleteRange(oldTags);

                List<StreetcodeTagDTO> newTagList = updateStreetcodeDTO.Tags.ToList();

                foreach (var tag in newTagList)
                {
                    var dbTag = await _repository.TagRepository
                        .GetFirstOrDefaultAsync(x => x.Id == tag.Id);

                    if (dbTag == null)
                    {
                        _logger.LogError(request, "tag not found");
                        continue;
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

        private UpdateStreetcodeDto ChoseStreetcodeType(string type, UpdateStreetcodeCommand request)
        {
            switch (type)
            {
                case "Person":
                    return JsonSerializer.Deserialize<UpdatePersonStreetcodeDto>(request.rawJsonUpdateDTO.GetRawText());
                case "Event":
                    return JsonSerializer.Deserialize<UpdateEventStreetcodeDto>(request.rawJsonUpdateDTO.GetRawText());
                default:
                    const string errorMsg = "Invalid streetcode type";
                    _logger.LogError(request, errorMsg);
                    throw new InvalidOperationException(errorMsg);
            }
        }
    }
}
