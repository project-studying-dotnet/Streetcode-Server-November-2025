using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.Streetcode.Comments;
using Streetcode.BLL.MediatR.Streetcode.Comments.Create;
using Streetcode.BLL.MediatR.Streetcode.Comments.Delete;
using Streetcode.BLL.MediatR.Streetcode.Comments.GetById;
using Streetcode.BLL.MediatR.Streetcode.Comments.GetByStreetcodeId;
using Streetcode.BLL.MediatR.Streetcode.Comments.Update;

namespace Streetcode.WebApi.Controllers.Streetcode
{
    public class CommentsController : BaseApiController
    {
        [HttpGet("{streetcodeId:int}")]
        public async Task<IActionResult> GetByStreetcodeId(int streetcodeId)
        {
            return HandleResult(await Mediator.Send(new GetCommentsByStreetcodeIdQuery(streetcodeId)));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return HandleResult(await Mediator.Send(new GetCommentByIdQuery(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto comment)
        {
            return HandleResult(await Mediator.Send(new CreateCommentCommand(comment)));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCommentDto updatedComment)
        {
            return HandleResult(await Mediator.Send(new UpdateCommentCommand(updatedComment)));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return HandleResult(await Mediator.Send(new DeleteCommentCommand(id)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdWithReplies([FromRoute] int id)
		{
			return HandleResult(await Mediator.Send(new GetCommentByIdWithRepliesQuery(id)));
		}
	}
}
