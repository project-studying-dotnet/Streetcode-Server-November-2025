using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specifications.Comments;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.Delete
{
    public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, Result<Unit>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public DeleteCommentHandler(IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repositoryWrapper.CommentsRepository
                .GetFirstOrDefaultAsync(c => c.Id == request.CommentId);

            if (comment == null)
            {
                var errorMsg = string.Format(
                    ErrorMessages.CommentNotFoundById,
                    request.CommentId);
                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            var parentId = comment.ParentCommentId;
            var hasChildren = await HasAliveChildren(comment.Id);

            if (hasChildren)
            {
                comment.IsDeleted = true;
                comment.DeletedAt = DateTime.UtcNow;
                _repositoryWrapper.CommentsRepository.Update(comment);
            }
            else
            {
                _repositoryWrapper.CommentsRepository.Delete(comment);
            }

            if (parentId.HasValue)
            {
                await CleanupParentChain(parentId.Value);
            }

            var success = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (!success)
            {
                var errorMsg = string.Format(
                    ErrorMessages.CommentDeletionFailed,
                    request.CommentId);
                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            return Result.Ok(Unit.Value);
        }

        private async Task<bool> HasAliveChildren(int parentId)
        {
            var spec = new CommentWithActiveChildrenSpecification(parentId);

            return await _repositoryWrapper.CommentsRepository.AnyAsync(spec);
        }

        private async Task CleanupParentChain(int parentId)
        {
            var parent = await _repositoryWrapper.CommentsRepository.GetFirstOrDefaultAsync(c => c.Id == parentId);

            if (parent != null && parent.IsDeleted)
            {
                var hasChildren = await HasAliveChildren(parent.Id);
                if (!hasChildren)
                {
                    var grandParentId = parent.ParentCommentId;
                    _repositoryWrapper.CommentsRepository.Delete(parent);

                    if (grandParentId.HasValue)
                    {
                        await CleanupParentChain(grandParentId.Value);
                    }
                }
            }
        }
    }
}