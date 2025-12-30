using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.TextContent;
using Streetcode.BLL.MediatR.Term.Create;
using Streetcode.BLL.MediatR.Term.GetAll;
using Streetcode.BLL.MediatR.Term.GetById;

namespace Streetcode.WebApi.Controllers.TextContent;

public class TermController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await Mediator.Send(new GetAllTermsQuery()));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        return HandleResult(await Mediator.Send(new GetTermByIdQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TermDto term)
    {
        return HandleResult(await Mediator.Send(new CreateTermCommand(term)));
    }
}
