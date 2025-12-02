using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode;
using Streetcode.BLL.DTO.Streetcode.Types;
using Streetcode.BLL.Interfaces.Logging;
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

        public CreateStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<JsonElement>> Handle(CreateStreetcodeCommand request, CancellationToken cancellationToken)
        {
            var rawJson = request.rawJsonCreateDTO;

            var streetcodeType = rawJson.GetProperty("StreetcodeType").GetString();

            CreateStreetcodeDTO сreateStreetcodeDTO;

            сreateStreetcodeDTO = ChoseStreetcodeType(streetcodeType, request);

            var streetcodeContent = _mapper.Map<StreetcodeContent>(сreateStreetcodeDTO);

            var result = _repository.StreetcodeRepository.Create(streetcodeContent);

            await _repository.SaveChangesAsync();

            if (сreateStreetcodeDTO.AudioId > 0)
            {
                var audio = await _repository.AudioRepository.GetFirstOrDefaultAsync(
                    x => x.Id == сreateStreetcodeDTO.AudioId);

                if (audio == null)
                {
                    _logger.LogError(request, "audio not found");
                }

                streetcodeContent.AudioId = audio.Id;
            }

            if (сreateStreetcodeDTO.Images != null)
            {
                foreach (var img in сreateStreetcodeDTO.Images)
                {
                    var image = await _repository.ImageRepository.GetFirstOrDefaultAsync(x => x.Id == img.ImageId);

                    if (image == null)
                    {
                        _logger.LogError(request, "image not found");
                    }

                    await _repository.StreetcodeImageRepository.CreateAsync(new StreetcodeImage()
                    {
                        ImageId = img.ImageId,
                        StreetcodeId = streetcodeContent.Id
                    });

                    var imgDetail = _mapper.Map<ImageDetails>(img);
                    await _repository.ImageDetailsRepository.CreateAsync(imgDetail);
                }
            }

            if (сreateStreetcodeDTO.Tags != null)
            {
                foreach (var tag in сreateStreetcodeDTO.Tags)
                {
                    var thisTag = await _repository.TagRepository.GetFirstOrDefaultAsync(x => x.Id == tag.Id);
                }
            }

            var resultIsSuccess = await _repository.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                var streetcodeDTO = _mapper.Map<CreateStreetcodeDTO>(result);
                var jsonResult = JsonSerializer.SerializeToElement(streetcodeDTO);
                return await Task.FromResult(Result.Ok(jsonResult));
            }
            else
            {
                const string errorMsg = "Failed to create a streetcode";
                _logger.LogError(request, errorMsg);
                return await Task.FromResult(Result.Fail<JsonElement>(new Error(errorMsg)));
            }
        }

        private CreateStreetcodeDTO ChoseStreetcodeType(string type, CreateStreetcodeCommand request)
        {
            switch (type)
            {
                case "Person":
                    return JsonSerializer
                        .Deserialize<CreatePersonStreetcodeDTO>(request.rawJsonCreateDTO.GetRawText());
                case "Event":
                    return JsonSerializer
                        .Deserialize<CreateEventStreetcodeDTO>(request.rawJsonCreateDTO.GetRawText());
                default:
                    const string errorMsg = "Failed to create a streetcode";
                    _logger.LogError(request, errorMsg);
                    throw new InvalidOperationException(errorMsg);
            }
        }
    }
}
