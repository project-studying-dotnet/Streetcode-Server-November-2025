using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.DTO.Streetcode.Comments;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Streetcode.Comments.Create;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Comments.Create
{
    public class CreateCommentHandler : IRequestHandler<CreateCommentCommand, Result<CommentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateCommentHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CommentDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var streetcodeExists =
                await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(s =>
                    s.Id == request.newComment.StreetcodeId);
            if (streetcodeExists is null)
            {
                var errorMsg = string.Format(ErrorMessages.StreetcodeNotFoundById, request.newComment.StreetcodeId);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var newComment = _mapper.Map<Comment>(request.newComment);
            if (newComment is null)
            {
                var errorMsg = ErrorMessages.CreateCommentMappingFailed;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            newComment = await _repositoryWrapper.CommentsRepository.CreateAsync(newComment);
            await _repositoryWrapper.SaveChangesAsync();
            return Result.Ok(_mapper.Map<CommentDto>(newComment));
        }
    }
}