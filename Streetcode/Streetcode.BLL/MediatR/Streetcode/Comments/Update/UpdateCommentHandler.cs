using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.Comments;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.Update
{
    public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, Result<CommentDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public UpdateCommentHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CommentDto>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _repositoryWrapper.CommentsRepository
                .GetFirstOrDefaultAsync(c => c.Id == request.comment.Id);

            if (comment == null)
            {
                var errorMsg = string.Format(ErrorMessages.CommentNotFoundById, request.comment.Id);
                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            _mapper.Map(request.comment, comment);
            comment.UpdatedAt = DateTime.UtcNow;

            _repositoryWrapper.CommentsRepository.Update(comment);

            var success = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (!success)
            {
                var errorMsg = string.Format(ErrorMessages.CommentUpdateFailed, request.comment.Id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var responseDto = _mapper.Map<CommentDto>(comment);
            return Result.Ok(responseDto);
        }
    }
}