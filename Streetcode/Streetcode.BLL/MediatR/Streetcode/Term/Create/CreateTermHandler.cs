using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Term.Create;

public class CreateTermHandler : IRequestHandler<CreateTermCommand, Result<TermDTO>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _wrapper;
    private readonly ILoggerService _logger;

    public CreateTermHandler(IMapper mapper, IRepositoryWrapper wrapper, ILoggerService logger)
    {
        _mapper = mapper;
        _wrapper = wrapper;
        _logger = logger;
    }

    public async Task<Result<TermDTO>> Handle(CreateTermCommand request, CancellationToken cancellationToken)
    {
        var newTerm = _mapper.Map<DAL.Entities.Streetcode.TextContent.Term>(request.term);

        if (newTerm is null)
        {
            const string errorMsg = "Can not convert null to Term";
            _logger.LogError(request, errorMsg);
            return Result.Fail(errorMsg);
        }

        var createdTerm = await _wrapper.TermRepository.CreateAsync(newTerm);
        var resultDto = _mapper.Map<TermDTO>(createdTerm);
        return Result.Ok(resultDto);
    }
}