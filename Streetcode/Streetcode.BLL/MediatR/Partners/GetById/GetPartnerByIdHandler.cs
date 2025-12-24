using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.DTO.Partners;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specifications.Partners;

namespace Streetcode.BLL.MediatR.Partners.GetById;

public class GetPartnerByIdHandler : IRequestHandler<GetPartnerByIdQuery, Result<PartnerDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetPartnerByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<PartnerDto>> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
    {
        var specification = new PartnerByIdSpecification(request.Id);

        var partner = await _repositoryWrapper
            .PartnersRepository
            .GetBySpecAsync(specification);

        if (partner is null)
        {
            string errorMsg = string.Format(ErrorMessages.PartnerNotFoundById, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(_mapper.Map<PartnerDto>(partner));
    }
}