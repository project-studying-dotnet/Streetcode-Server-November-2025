using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Fact.Create;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Fact.Delete
{
    public class DeleteFactHandler : IRequestHandler<DeleteFactCommand, Result<Unit>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteFactHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteFactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedFact = await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(f => f.Id == request.id);

                if (deletedFact is null)
                {
                    return Result.Fail("Fact not found");
                }

                _repositoryWrapper.FactRepository.Delete(deletedFact);

                await _repositoryWrapper.SaveChangesAsync();
                return Result.Ok(Unit.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ex.Message);
            }
        }
    }
}
