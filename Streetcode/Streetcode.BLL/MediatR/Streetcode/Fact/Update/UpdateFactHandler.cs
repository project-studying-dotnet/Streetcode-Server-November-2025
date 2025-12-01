using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Fact.Update;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Fact.Update
{
    public class UpdateFactHandler : IRequestHandler<UpdateFactCommand, Result<FactDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateFactHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<FactDto>> Handle(UpdateFactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingFact = await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(f => f.Id == request.updateFact.Id);

                if (existingFact is null)
                {
                    return Result.Fail("Fact not found");
                }

                _mapper.Map(request.updateFact, existingFact);

                _repositoryWrapper.FactRepository.Update(existingFact);
                await _repositoryWrapper.SaveChangesAsync();

                return Result.Ok(_mapper.Map<FactDto>(existingFact));
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ex.Message);
            }
        }
    }
}
