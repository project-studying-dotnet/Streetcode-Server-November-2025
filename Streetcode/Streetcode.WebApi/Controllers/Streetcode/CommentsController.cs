using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.MediatR.Streetcode.Comments.GetByStreetcodeId;

namespace Streetcode.WebApi.Controllers.Streetcode
{
    public class CommentsController : BaseApiController
    {
        [HttpGet("{streetcodeId:int}")]
        public async Task<IActionResult> GetByStreetcodeId(int streetcodeId)
        {
            return HandleResult(await Mediator.Send(new GetCommentsByStreetcodeIdQuery(streetcodeId)));
        }
    }
}
