using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Create
{
    using DAL.Entities.Timeline;

    public class CreateHistoricalContextHandler : IRequestHandler<CreateHistoricalContextCommand, Result<HistoricalContextDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateHistoricalContextHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<HistoricalContextDto>> Handle(CreateHistoricalContextCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingContext = await _repositoryWrapper.HistoricalContextRepository
                    .GetFirstOrDefaultAsync(hc => hc.Title == request.HistoricalContext.Title);

                if (existingContext is not null)
                {
                    const string errorMsg = "Historical context with the same title already exists";
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                var newContext = _mapper.Map<HistoricalContext>(request.HistoricalContext);
                newContext = await _repositoryWrapper.HistoricalContextRepository.CreateAsync(newContext);
                await _repositoryWrapper.SaveChangesAsync();

                return Result.Ok(_mapper.Map<HistoricalContextDto>(newContext));
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ex.Message);
            }
        }
    }
}
