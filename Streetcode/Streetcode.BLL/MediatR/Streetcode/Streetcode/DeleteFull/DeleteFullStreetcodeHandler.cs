using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Cache;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.DeleteFull
{
    public class DeleteFullStreetcodeHandler : IRequestHandler<DeleteFullStreetcodeCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        private readonly ICacheService _cacheService;

        public DeleteFullStreetcodeHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger, ICacheService cacheService)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _cacheService = cacheService;
        }

        public async Task<Result<Unit>> Handle(DeleteFullStreetcodeCommand request, CancellationToken cancellationToken)
        {
            var streetcode = await _repositoryWrapper.StreetcodeRepository
            .GetFirstOrDefaultAsync(f => f.Id == request.Id);

            if (streetcode is null)
            {
                string errorMsg = $"Cannot find a streetcode with corresponding categoryId: {request.Id}";
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var streetcodeTags = _repositoryWrapper.StreetcodeTagIndexRepository
                .GetAllAsync(t => t.StreetcodeId == request.Id).Result.ToList();

            var streetcodeImages = _repositoryWrapper.StreetcodeImageRepository
                .GetAllAsync(i => i.StreetcodeId == request.Id).Result.ToList();

            var imgIds = streetcodeImages.Select(i => i.ImageId).ToList();
            var imageDtails = _repositoryWrapper.ImageDetailsRepository
                .GetAllAsync(id => imgIds.Contains(id.ImageId)).Result.ToList();

            var audio = await _repositoryWrapper.AudioRepository
                .GetFirstOrDefaultAsync(a => a.Id == streetcode.AudioId);

            _repositoryWrapper.StreetcodeTagIndexRepository.DeleteRange(streetcodeTags);
            _repositoryWrapper.StreetcodeImageRepository.DeleteRange(streetcodeImages);
            _repositoryWrapper.ImageDetailsRepository.DeleteRange(imageDtails);
            if (audio != null)
            {
                _repositoryWrapper.AudioRepository.Delete(audio);
            }

            _repositoryWrapper.StreetcodeRepository.Delete(streetcode);

            var resultIsDeleteSucces = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsDeleteSucces)
            {
                await _cacheService.RemoveAsync($"Streetcode_{request.Id}");

                return Result.Ok(Unit.Value);
            }
            else
            {
                const string errorMsg = "Failed to delete streetcode fully";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
