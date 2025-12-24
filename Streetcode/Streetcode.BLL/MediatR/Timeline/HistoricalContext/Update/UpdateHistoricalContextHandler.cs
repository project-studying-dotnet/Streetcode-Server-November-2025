using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Timeline;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Timeline.HistoricalContext.Update
{
    public class UpdateHistoricalContextHandler : IRequestHandler<UpdateHistoricalContextCommand, Result<HistoricalContextDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateHistoricalContextHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<HistoricalContextDto>> Handle(UpdateHistoricalContextCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingContext = await _repositoryWrapper.HistoricalContextRepository
                    .GetFirstOrDefaultAsync(hc => hc.Id == request.HistoricalContext.Id);

                if (existingContext is null)
                {
                    var errorMsg = string.Format(ErrorMessages.HistoricalContextNotFoundById, request.HistoricalContext.Id);
                    _logger.LogError(request, errorMsg);
                    return Result.Fail(errorMsg);
                }

                if (existingContext.Title != request.HistoricalContext.Title)
                {
                    var duplicateTitle = await _repositoryWrapper.HistoricalContextRepository
                        .GetFirstOrDefaultAsync(hc => hc.Title == request.HistoricalContext.Title);

                    if (duplicateTitle is not null)
                    {
                        var errorMsg = ErrorMessages.HistoricalContextTitleAlreadyExists;
                        _logger.LogError(request, errorMsg);
                        return Result.Fail(errorMsg);
                    }
                }

                _mapper.Map(request.HistoricalContext, existingContext);
                _repositoryWrapper.HistoricalContextRepository.Update(existingContext);
                await _repositoryWrapper.SaveChangesAsync();

                return Result.Ok(_mapper.Map<HistoricalContextDto>(existingContext));
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ErrorMessages.HistoricalContextUpdateFailed);
            }
        }
    }
}
