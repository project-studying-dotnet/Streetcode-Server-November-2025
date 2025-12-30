using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.BLL.DTO.Streetcode.Comments;

namespace Streetcode.BLL.MediatR.Streetcode.Comments.GetById
{
	public class GetCommentByIdWithRepliesHandler : IRequestHandler<GetCommentByIdWithRepliesQuery, Result<CommentWithRepliesDto>>
	{
		private readonly IRepositoryWrapper _repositoryWrapper;
		private readonly IMapper _mapper;

		public GetCommentByIdWithRepliesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
		{
			_repositoryWrapper = repositoryWrapper;
			_mapper = mapper;
		}

		public async Task<Result<CommentWithRepliesDto>> Handle(GetCommentByIdWithRepliesQuery request, CancellationToken cancellationToken)
		{
			var comment = await _repositoryWrapper.CommentsRepository.GetFirstOrDefaultAsync(
				predicate: c => c.Id == request.Id,
				include: i => i.Include(c => c.Replies));

			if (comment is null)
			{
				return Result.Fail($"Comment with id {request.Id} not found");
			}

			var responseDto = _mapper.Map<CommentWithRepliesDto>(comment);

			return Result.Ok(responseDto);
		}
	}
}