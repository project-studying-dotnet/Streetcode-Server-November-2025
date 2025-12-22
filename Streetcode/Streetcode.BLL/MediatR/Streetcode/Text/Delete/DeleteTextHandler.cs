using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Text.Delete;

public class DeleteTextHandler : IRequestHandler<DeleteTextCommand, Result<Unit>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public DeleteTextHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteTextCommand request, CancellationToken cancellationToken)
    {
        var text = await _repositoryWrapper.TextRepository.GetFirstOrDefaultAsync(f => f.Id == request.Id);

        if (text is null)
        {
            var errorMsg = string.Format(ErrorMessages.TextNotFoundById, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        _repositoryWrapper.TextRepository.Delete(text);
        var isSuccessResult = await _repositoryWrapper.SaveChangesAsync() > 0;

        if (!isSuccessResult)
        {
            var errorMsg = ErrorMessages.CannotSaveChangesInDatabase;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return Result.Ok(Unit.Value);
    }
}