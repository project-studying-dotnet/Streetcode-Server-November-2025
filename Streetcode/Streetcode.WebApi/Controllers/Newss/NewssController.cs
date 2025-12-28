using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streetcode.BLL.DTO.News;
using Streetcode.BLL.MediatR.Newss.Create;
using Streetcode.BLL.MediatR.Newss.Delete;
using Streetcode.BLL.MediatR.Newss.GetAll;
using Streetcode.BLL.MediatR.Newss.GetById;
using Streetcode.BLL.MediatR.Newss.GetByUrl;
using Streetcode.BLL.MediatR.Newss.GetNewsAndLinksByUrl;
using Streetcode.BLL.MediatR.Newss.SortedByDateTime;
using Streetcode.BLL.MediatR.Newss.Update;
using Streetcode.DAL.Enums;

namespace Streetcode.WebApi.Controllers.Newss;

public class NewsController : BaseApiController
{
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		return HandleResult(await Mediator.Send(new GetAllNewsQuery()));
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById([FromRoute] int id)
	{
		return HandleResult(await Mediator.Send(new GetNewsByIdQuery(id)));
	}

	[HttpGet("byUrl/{url}")]
	public async Task<IActionResult> GetByUrl([FromRoute] string url)
	{
		return HandleResult(await Mediator.Send(new GetNewsByUrlQuery(url)));
	}

	[HttpGet("withLinks/{url}")]
	public async Task<IActionResult> GetNewsAndLinksByUrl([FromRoute] string url)
	{
		return HandleResult(await Mediator.Send(new GetNewsAndLinksByUrlQuery(url)));
	}

	[HttpGet("sorted")]
	public async Task<IActionResult> SortedByDateTime()
	{
		return HandleResult(await Mediator.Send(new SortedByDateTimeQuery()));
	}

	[HttpPost]
	[Authorize(Roles = nameof(UserRole.Administrator))]
	public async Task<IActionResult> Create([FromBody] NewsDto news)
	{
		return HandleResult(await Mediator.Send(new CreateNewsCommand(news)));
	}

	[HttpPut]
	[Authorize(Roles = nameof(UserRole.Administrator))]
	public async Task<IActionResult> Update([FromBody] NewsDto news)
	{
		return HandleResult(await Mediator.Send(new UpdateNewsCommand(news)));
	}

	[HttpDelete("{id:int}")]
	[Authorize(Roles = nameof(UserRole.Administrator))]
	public async Task<IActionResult> Delete([FromRoute] int id)
	{
		return HandleResult(await Mediator.Send(new DeleteNewsCommand(id)));
	}
}