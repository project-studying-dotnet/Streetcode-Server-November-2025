using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.Streetcode.TextContent;
using Streetcode.BLL.MediatR.Streetcode.Term.Create;
using Streetcode.BLL.MediatR.Streetcode.Term.GetAll;
using Streetcode.BLL.MediatR.Streetcode.Term.GetById;
using Streetcode.DAL.Enums;

namespace Streetcode.WebApi.Controllers.Streetcode.TextContent;

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
    [Authorize(Roles = nameof(UserRole.Administrator))]
    public async Task<IActionResult> Create([FromBody] TermDto term)
    {
        return HandleResult(await Mediator.Send(new CreateTermCommand(term)));
    }
}
