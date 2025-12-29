using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specifications.Team;

namespace Streetcode.BLL.MediatR.Team.GetAll
{
    public class GetAllTeamHandler : IRequestHandler<GetAllTeamQuery, Result<IEnumerable<TeamMemberDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllTeamHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<TeamMemberDto>>> Handle(GetAllTeamQuery request, CancellationToken cancellationToken)
        {
            var spec = new AllTeamSpecification();
            var team = await _repositoryWrapper.TeamRepository.ListAsync(spec, cancellationToken);

            if (team is null)
            {
                var errorMsg = ErrorMessages.TeamNotFound;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<TeamMemberDto>>(team));
        }
    }
}