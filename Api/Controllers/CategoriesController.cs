using Application.Category.Dto;
using Application.Common.Commands;
using Application.Common.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(IMediator mediator) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> Index([FromQuery] List<string> select, CancellationToken cancellationToken)
	{
		var categories = await mediator.Send(new FindAllQuery<CategoryResponse>(select), cancellationToken);

		return Ok(categories);
	}

	[HttpGet("{slug}")]
	public async Task<IActionResult> FindOne(string slug, CancellationToken cancellationToken)
	{
		var category = await mediator.Send(new FindOneQuery<CategoryResponse>(slug), cancellationToken);
		return Ok(category);
	}

	[HttpPost]
	public async Task<IActionResult> Create(
		[FromBody] CreateCommand<CreateCategoryRequest, CategoryResponse> categoryRequest,
		CancellationToken cancellationToken)
	{
		var category = await mediator.Send(categoryRequest, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id = category.Id }, category);
	}

	[HttpPatch("{slug}")]
	public async Task<IActionResult> Update(string slug,
		[FromBody] UpdateCommand<UpdateCategoryRequest, CategoryResponse> categoryRequest,
		CancellationToken cancellationToken)
	{
		var category = await mediator.Send(categoryRequest, cancellationToken);

		return Ok(category);
	}

	[HttpDelete("{slug}")]
	public async Task<IActionResult> Delete(string slug, CancellationToken cancellationToken)
	{
		var category = await mediator.Send(new DeleteCommand<CategoryResponse>(slug), cancellationToken);

		return Ok(category);
	}
}