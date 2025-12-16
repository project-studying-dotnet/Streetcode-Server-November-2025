using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Rewrite;
using Streetcode.BLL.DTO.AdditionalContent.Subtitles;
using Streetcode.BLL.DTO.Sources;
using Streetcode.BLL.DTO.Transactions;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.ResultVariations;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Transactions.TransactionLink.GetByStreetcodeId;

public class GetTransactLinkByStreetcodeIdHandler : IRequestHandler<GetTransactLinkByStreetcodeIdQuery, Result<TransactLinkDto?>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetTransactLinkByStreetcodeIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<TransactLinkDto?>> Handle(GetTransactLinkByStreetcodeIdQuery request, CancellationToken cancellationToken)
    {
        var transactLink = await _repositoryWrapper.TransactLinksRepository
            .GetFirstOrDefaultAsync(f => f.StreetcodeId == request.StreetcodeId);

        if (transactLink is null)
        {
            if (await _repositoryWrapper.StreetcodeRepository
                .GetFirstOrDefaultAsync(s => s.Id == request.StreetcodeId) == null)
            {
                var errorMsg = string.Format(ErrorMessages.TransactionLinkNotFoundByStreetcodeId, request.StreetcodeId);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }

        NullResult<TransactLinkDto?> result = new NullResult<TransactLinkDto?>();
        result.WithValue(_mapper.Map<TransactLinkDto?>(transactLink));
        return result;
    }
}