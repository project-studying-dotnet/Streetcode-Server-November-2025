using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Update;

public class UpdateTextHandler : IRequestHandler<UpdateTextCommand, Result<TextDTO>>
{
    private const string DefaultAuthorship = "Текст підготовлений спільно з";
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public UpdateTextHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<TextDTO>> Handle(UpdateTextCommand request, CancellationToken cancellationToken)
    {
        var text = await _repositoryWrapper.TextRepository.GetFirstOrDefaultAsync(f => f.Id == request.Id);

        if (text is null)
        {
            const string errorMsg = "Cannot find text with corresponding id.";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        text = _mapper.Map(request.Text, text);

        if (!string.IsNullOrEmpty(text.Authorship) && text.Authorship.Trim() == DefaultAuthorship)
        {
            text.Authorship = null;
        }

        _repositoryWrapper.TextRepository.Update(text);
        var isSuccessResult = await _repositoryWrapper.SaveChangesAsync() > 0;

        if (!isSuccessResult)
        {
            const string errorMsg = "Cannot save changes in the database.";
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var resultDto = _mapper.Map<TextDTO>(text);
        return Result.Ok(resultDto);
    }
}