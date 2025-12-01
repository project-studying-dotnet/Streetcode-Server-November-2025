using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Fact.Create;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Fact.Create
{
    public class CreateFactHandler : IRequestHandler<CreateFactCommand, Result<FactDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateFactHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<FactDto>> Handle(CreateFactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newFact = _mapper.Map<DAL.Entities.Streetcode.TextContent.Fact>(request.newFact);

                if (newFact == null)
                {
                    return Result.Fail("Mapped Fact entity is null");
                }

                var existedFact = await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(f => f.Title == newFact.Title);

                if (existedFact is not null)
                {
                    return Result.Fail("Fact with the same title already exists");
                }

                newFact = await _repositoryWrapper.FactRepository.CreateAsync(newFact);
                await _repositoryWrapper.SaveChangesAsync();
                return Result.Ok(_mapper.Map<FactDto>(newFact));
            }
            catch (Exception ex)
            {
                _logger.LogError(request, ex.Message);
                return Result.Fail(ex.Message);
            }
        }
    }
}