using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.DTO.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.TimelineItem.Create
{
    using DAL.Entities.Timeline;

    public class CreateTimelineItemHandler : IRequestHandler<CreateTimelineItemCommand, Result<TimelineItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateTimelineItemHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<TimelineItemDto>> Handle(CreateTimelineItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
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

                var newTimelineItem = _mapper.Map<TimelineItem>(request.TimelineItem);

                newTimelineItem.HistoricalContextTimelines = request.TimelineItem.HistoricalContextIds
                    .Select(id => new HistoricalContextTimeline
                    {
                        HistoricalContextId = id,
                        Timeline = newTimelineItem
                    }).ToList();

                newTimelineItem = await _repositoryWrapper.TimelineRepository.CreateAsync(newTimelineItem);
                var saveResult = await _repositoryWrapper.SaveChangesAsync();

                if (saveResult <= 0)
                {
                    var errorMsg = "Failed to save the timeline item to the database";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                var result = await _repositoryWrapper.TimelineRepository
                    .GetFirstOrDefaultAsync(
                        predicate: t => t.Id == newTimelineItem.Id,
                        include: query => query
                            .Include(t => t.HistoricalContextTimelines)
                            .ThenInclude(hct => hct.HistoricalContext));

                if (result == null)
                {
                    var errorMsg = "Failed to find the created timeline item";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                var dto = _mapper.Map<TimelineItemDto>(result);
                if (dto == null)
                {
                    var errorMsg = "Failed to map timeline item to DTO";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                return Result.Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ErrorMessages.TimelineItemCreationFailed);
            }
        }
    }
}
