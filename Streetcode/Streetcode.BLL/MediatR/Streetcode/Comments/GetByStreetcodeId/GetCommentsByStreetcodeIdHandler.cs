using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.Comments;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.GetByStreetcodeId
{
    public class GetCommentsByStreetcodeIdHandler : IRequestHandler<GetCommentsByStreetcodeIdQuery, Result<IEnumerable<CommentDto>>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public GetCommentsByStreetcodeIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<CommentDto>>> Handle(GetCommentsByStreetcodeIdQuery request, CancellationToken cancellationToken)
        {
            var allComments = await _repositoryWrapper.CommentsRepository
                .GetAllAsync(c => c.StreetcodeId == request.streetcodeId);

            if (!allComments.Any())
            {
                _logger.LogDebug(string.Format(ErrorMessages.CommentsNotFoundByStreetcodeId, request.streetcodeId));
                return Result.Ok(Enumerable.Empty<CommentDto>());
            }

            var commentsByParentId = allComments.ToLookup(c => c.ParentCommentId);

            void BuildReplies(Comment parent)
            {
                parent.Replies = commentsByParentId[parent.Id].ToList();

                foreach (var reply in parent.Replies)
                {
                    BuildReplies(reply);
                }
            }

            var rootComments = allComments
                .Where(c => c.ParentCommentId == null)
                .ToList();

            foreach (var root in rootComments)
            {
                BuildReplies(root);
            }

            return Result.Ok(_mapper.Map<IEnumerable<CommentDto>>(rootComments));
        }
    }
}
