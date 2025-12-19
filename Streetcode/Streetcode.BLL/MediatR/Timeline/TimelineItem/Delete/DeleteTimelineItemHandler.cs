using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Delete
{
    public class DeleteTimelineItemHandler : IRequestHandler<DeleteTimelineItemCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteTimelineItemHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteTimelineItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var timelineItem = await _repositoryWrapper.TimelineRepository
                    .GetFirstOrDefaultAsync(t => t.Id == request.Id);

                if (timelineItem is null)
                {
                    var errorMsg = string.Format(ErrorMessages.TimelineItemNotFoundById, request.Id);
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                _repositoryWrapper.TimelineRepository.Delete(timelineItem);
                await _repositoryWrapper.SaveChangesAsync();

                return Result.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ErrorMessages.TimelineItemDeletionFailed);
            }
        }
    }
}
