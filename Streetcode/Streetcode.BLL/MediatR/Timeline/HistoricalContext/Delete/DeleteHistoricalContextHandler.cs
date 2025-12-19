using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Delete
{
    public class DeleteHistoricalContextHandler : IRequestHandler<DeleteHistoricalContextCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteHistoricalContextHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteHistoricalContextCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var historicalContext = await _repositoryWrapper.HistoricalContextRepository
                    .GetFirstOrDefaultAsync(hc => hc.Id == request.Id);

                if (historicalContext is null)
                {
                    var errorMsg = string.Format(ErrorMessages.HistoricalContextNotFoundById, request.Id);
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                _repositoryWrapper.HistoricalContextRepository.Delete(historicalContext);
                await _repositoryWrapper.SaveChangesAsync();

                return Result.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ErrorMessages.HistoricalContextDeletionFailed);
            }
        }
    }
}
