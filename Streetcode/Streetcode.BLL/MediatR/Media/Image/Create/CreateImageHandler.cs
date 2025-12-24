using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Media.Image.Create;

public class CreateImageHandler : IRequestHandler<CreateImageCommand, Result<ImageDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IBlobService _blobService;
    private readonly ILoggerService _logger;
    private static readonly string[] AllowedExtensions = { ".png", ".jpg", ".jpeg", ".webp" };

    public CreateImageHandler(
        IBlobService blobService,
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        ILoggerService logger)
    {
        _blobService = blobService;
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<ImageDto>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        if (!AllowedExtensions.Contains($".{request.Image.Extension.ToLower()}"))
        {
            var errorMsg = ErrorMessages.InvalidImageFormat;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        string hashBlobStorageName = _blobService.SaveFileInStorage(
            request.Image.BaseFormat,
            request.Image.Title,
            request.Image.MimeType);

        var image = _mapper.Map<DAL.Entities.Media.Images.Image>(request.Image);

        image.BlobName = $"{hashBlobStorageName}.{request.Image.Extension}";

        await _repositoryWrapper.ImageRepository.CreateAsync(image);
        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

        var createdImage = _mapper.Map<ImageDto>(image);

        createdImage.Base64 = request.Image.BaseFormat;

        if(resultIsSuccess)
        {
            return Result.Ok(createdImage);
        }
        else
        {
            var errorMsg = ErrorMessages.ImageCreationFailed;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}
