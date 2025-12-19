using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Toponyms.Delete
{
    public class DeleteStreetcodeToponymHandler
        : IRequestHandler<DeleteStreetcodeToponymCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteStreetcodeToponymHandler(
            IRepositoryWrapper repositoryWrapper,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(
            DeleteStreetcodeToponymCommand request,
            CancellationToken cancellationToken)
        {
            var streetcodeToponym = await _repositoryWrapper.StreetcodeToponymRepository
                .GetFirstOrDefaultAsync(st =>
                    st.StreetcodeId == request.StreetcodeId &&
                    st.ToponymId == request.ToponymId);

            if (streetcodeToponym is null)
            {
                string errorMsg = $"Cannot find relationship with StreetcodeId={request.StreetcodeId} and ToponymId={request.ToponymId}";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            _repositoryWrapper.StreetcodeToponymRepository.Delete(streetcodeToponym);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (!resultIsSuccess)
            {
                const string errorMsg = "Failed to delete streetcode-toponym relationship.";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(Unit.Value);
        }
    }
}
