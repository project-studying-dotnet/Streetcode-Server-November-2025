using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.DTO.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Update
{
    using DAL.Entities.Timeline;

    public class UpdateTimelineItemHandler : IRequestHandler<UpdateTimelineItemCommand, Result<TimelineItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateTimelineItemHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<TimelineItemDto>> Handle(UpdateTimelineItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingTimelineItem = await _repositoryWrapper.TimelineRepository
                    .GetFirstOrDefaultAsync(
                        predicate: t => t.Id == request.TimelineItem.Id,
                        include: query => query.Include(t => t.HistoricalContextTimelines));

                if (existingTimelineItem is null)
                {
                    var errorMsg = string.Format(ErrorMessages.TimelineItemNotFoundById, request.TimelineItem.Id);
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                var streetcodeExists = await _repositoryWrapper.StreetcodeRepository
                    .GetFirstOrDefaultAsync(s => s.Id == request.TimelineItem.StreetcodeId);

                if (streetcodeExists is null)
                {
                    var errorMsg = string.Format(ErrorMessages.StreetcodeNotFoundById, request.TimelineItem.StreetcodeId);
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                if (request.TimelineItem.HistoricalContextIds.Any())
                {
                    var existingContexts = await _repositoryWrapper.HistoricalContextRepository
                        .GetAllAsync(
                            predicate: hc => request.TimelineItem.HistoricalContextIds.Contains(hc.Id));

                    var existingContextIds = existingContexts.Select(hc => hc.Id).ToList();
                    var missingContextIds = request.TimelineItem.HistoricalContextIds
                        .Except(existingContextIds).ToList();

                    if (missingContextIds.Any())
                    {
                        var errorMsg = string.Format(ErrorMessages.HistoricalContextsNotFoundByIds, string.Join(", ", missingContextIds));
                        _logger.LogError(request, errorMsg);
                        return Result.Fail(errorMsg);
                    }
                }

                _mapper.Map(request.TimelineItem, existingTimelineItem);

                var oldRelationships = existingTimelineItem.HistoricalContextTimelines.ToList();
                foreach (var oldRel in oldRelationships)
                {
                    _repositoryWrapper.HistoricalContextTimelineRepository.Delete(oldRel);
                }

                existingTimelineItem.HistoricalContextTimelines = request.TimelineItem.HistoricalContextIds
                    .Select(id => new HistoricalContextTimeline
                    {
                        TimelineId = existingTimelineItem.Id,
                        HistoricalContextId = id
                    }).ToList();

                _repositoryWrapper.TimelineRepository.Update(existingTimelineItem);
                await _repositoryWrapper.SaveChangesAsync();

                var result = await _repositoryWrapper.TimelineRepository
                    .GetFirstOrDefaultAsync(
                        predicate: t => t.Id == existingTimelineItem.Id,
                        include: query => query
                            .Include(t => t.HistoricalContextTimelines)
                            .ThenInclude(hct => hct.HistoricalContext));

                return Result.Ok(_mapper.Map<TimelineItemDto>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ErrorMessages.TimelineItemUpdateFailed);
            }
        }
    }
}
