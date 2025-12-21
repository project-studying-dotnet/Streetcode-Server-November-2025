using System.Text.Json;
using System.Transactions;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Util;
using Streetcode.DAL.Entities.AdditionalContent;
using Streetcode.DAL.Entities.Media.Images;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Create
{
    public class CreateStreetcodeHandler : IRequestHandler<CreateStreetcodeCommand, Result<JsonElement>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly StreetcodeCreateHelper _streetcodeCreateHelper;

        public CreateStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _streetcodeCreateHelper = new StreetcodeCreateHelper(_logger);
        }

        public async Task<Result<JsonElement>> Handle(CreateStreetcodeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rawJson = request.rawJsonCreateDTO;

                int streetcodeIndex = rawJson.GetProperty("Index").GetInt32();

                if (await StreetcodeIndexExists(streetcodeIndex))
                {
                    return Result.Fail(new Error(string.Format(ErrorMessages.StreetcodeWithIndexAlreadyExists, streetcodeIndex)));
                }

                string streetcodeType = rawJson.GetProperty("StreetcodeType").GetString();

                CreateStreetcodeDto сreateStreetcodeDTO =
                    _streetcodeCreateHelper.ChoseStreetcodeType(streetcodeType, request);

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

                await _repository.SaveChangesAsync();

                var streetcodeDTO = _mapper.Map<CreateStreetcodeDto>(streetcodeContent);
                var jsonResult = JsonSerializer.SerializeToElement(streetcodeDTO);
                return Result.Ok(jsonResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(new Error(ex.Message));
            }
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
                _logger.LogError(request, ErrorMessages.AudioNotFound);
                return Result.Fail(ErrorMessages.AudioNotFound);
            }

            entity.AudioId = audio.Id;
            return Result.Ok();
        }

        private async Task<Result> HandleImagesCreate(CreateStreetcodeDto dto, StreetcodeContent entity, CreateStreetcodeCommand request)
        {
            if (dto.Images.IsNullOrEmpty())
            {
                return Result.Ok();
            }

            List<string> imageErrors = new List<string>();

            foreach (var img in dto.Images)
            {
                var image = await _repository.ImageRepository.GetFirstOrDefaultAsync(x => x.Id == img.ImageId);
                if (image is null)
                {
                    var errorMsg = string.Format(ErrorMessages.StreetcodeImageNotFoundById, img.ImageId);
                    _logger.LogError(request, errorMsg);
                    imageErrors.Add(errorMsg);
                    continue;
                }

                await _repository.StreetcodeImageRepository.CreateAsync(new StreetcodeImage
                {
                    ImageId = img.ImageId,
                    StreetcodeId = entity.Id
                });

                var imgDetail = _mapper.Map<ImageDetails>(img);
                await _repository.ImageDetailsRepository.CreateAsync(imgDetail);
            }

            if (imageErrors.Count > 0)
            {
                return Result.Fail(string.Join("; ", imageErrors));
            }

            return Result.Ok();
        }

        private async Task<Result> HandleTagsCreate(CreateStreetcodeDto dto, StreetcodeContent entity, CreateStreetcodeCommand request)
        {
            if (dto.Tags.IsNullOrEmpty())
            {
                return Result.Ok();
            }

            List<string> tagErrors = new List<string>();

            var tagList = dto.Tags.ToList();
            foreach (var tag in tagList)
            {
                var thisTag = await _repository.TagRepository.GetFirstOrDefaultAsync(x => x.Id == tag.Id);
                if (thisTag is null)
                {
                    var errorMsg = string.Format(ErrorMessages.TagNotFoundById, tag.Id);
                    _logger.LogError(request, errorMsg);
                    tagErrors.Add(errorMsg);
                    continue;
                }

                await _repository.StreetcodeTagIndexRepository.CreateAsync(new StreetcodeTagIndex
                {
                    StreetcodeId = entity.Id,
                    TagId = tag.Id,
                    IsVisible = tag.IsVisible,
                    Index = tagList.IndexOf(tag)
                });
            }

            if (tagErrors.Count > 0)
            {
                return Result.Fail(string.Join("; ", tagErrors));
            }

            return Result.Ok();
        }
    }
}
