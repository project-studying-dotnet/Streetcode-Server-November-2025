using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.DTO.Team;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Partners.GetById;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specifications.Team;

namespace Streetcode.BLL.MediatR.Team.GetById
{
    public class GetByIdTeamHandler : IRequestHandler<GetByIdTeamQuery, Result<TeamMemberDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetByIdTeamHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<TeamMemberDto>> Handle(GetByIdTeamQuery request, CancellationToken cancellationToken)
        {
            var spec = new TeamMemberByIdSpecification(request.Id);
            var team = await _repositoryWrapper
                .TeamRepository
                .GetBySpecAsync(spec, cancellationToken);

            if (team is null)
            {
                var errorMsg = string.Format(ErrorMessages.TeamNotFoundById, request.Id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<TeamMemberDto>(team));
        }
    }
}