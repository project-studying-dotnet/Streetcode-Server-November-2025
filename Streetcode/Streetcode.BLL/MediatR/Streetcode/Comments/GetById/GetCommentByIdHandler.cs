using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.Comments;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.GetById
{
    public class GetCommentByIdHandler : IRequestHandler<GetCommentByIdQuery, Result<CommentDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public GetCommentByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CommentDto>> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var spec = new CommentWithRepliesSpecification(request.id);
            var comment = await _repositoryWrapper.CommentsRepository
                .GetBySpecAsync(spec, cancellationToken);

            if (comment is null)
            {
                var errorMsg = string.Format(ErrorMessages.CommentNotFoundById, request.id);
                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            return Result.Ok(_mapper.Map<CommentDto>(comment));
        }
    }
}
