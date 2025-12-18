using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.TextContent.Text;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

using Entity = Streetcode.DAL.Entities.Streetcode.TextContent.Text;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Create;

public class CreateTextHandler : IRequestHandler<CreateTextCommand, Result<TextDto>>
{
    private const string DefaultAuthorship = "Текст підготовлений спільно з";

    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;
    private readonly ILoggerService _logger;

    public CreateTextHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<TextDto>> Handle(CreateTextCommand request, CancellationToken cancellationToken)
    {
        var textDto = request.Text;

        if (textDto is null)
        {
            var errorMsg = ErrorMessages.TextDataRequired;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var text = _mapper.Map<Entity>(textDto);

        if (text is null)
        {
            var errorMsg = ErrorMessages.CannotMapEntity;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        if (!string.IsNullOrEmpty(text.AdditionalText) && text.AdditionalText.Trim() == DefaultAuthorship)
        {
            text.AdditionalText = null;
        }

        var createdText = await _repositoryWrapper.TextRepository.CreateAsync(text);
        var isSuccessResult = await _repositoryWrapper.SaveChangesAsync() > 0;

        if (!isSuccessResult)
        {
            var errorMsg = ErrorMessages.CannotSaveChangesInDatabase;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var resultDto = _mapper.Map<TextDto>(createdText);
        return Result.Ok(resultDto);
    }
}