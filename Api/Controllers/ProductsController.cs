using Api.Mapping;
using Api.QueryParams;
using Api.Validators;
using Application.Common.Commands;
using Application.Common.Criteria;
using Application.Common.Queries;
using Application.Product.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController(IMediator mediator, IQueryOptionsValidator<ProductResponse> queryOptionsValidator)
	: ControllerBase
{
	[HttpGet]
	public async Task<IActionResult>
		Index(ListQueryOptions<ProductResponse> options,
			CancellationToken cancellationToken)
	{
		var categories = await mediator.Send(
			new FindAllQuery<ProductResponse>(new ListQueryCriteria { Include = [], Select = [], Sort = [] }),
			cancellationToken);

		return Ok(categories);
	}

	[HttpGet("{slug}")]
	public async Task<IActionResult> FindOne(string slug,
		[FromQuery] QueryOptions<ProductResponse> options,
		[FromServices] IQueryOptionsMapper<ProductResponse> mapper,
		CancellationToken cancellationToken)
	{
		var validationResult = await queryOptionsValidator.ValidateAsync(options, cancellationToken);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors);

		QueryCriteria criteria = mapper.Map(options);
		var product = await mediator.Send(new FindOneBySlugQuery<ProductResponse>(slug, criteria), cancellationToken);
		return Ok(product);
	}

	[HttpPost]
	public async Task<IActionResult> Create(
		[FromBody] CreateCommand<CreateProductRequest, ProductResponse> productRequest,
		CancellationToken cancellationToken)
	{
		var product = await mediator.Send(productRequest, cancellationToken);
		return CreatedAtAction(nameof(Create), new { id = product.Id }, product);
	}

	[HttpPatch("{slug}")]
	public async Task<IActionResult> Update(string slug,
		[FromBody] UpdateCommand<UpdateProductRequest, ProductResponse> productRequest,
		CancellationToken cancellationToken)
	{
		var product = await mediator.Send(productRequest, cancellationToken);

		return Ok(product);
	}

	[HttpDelete("{slug}")]
	public async Task<IActionResult> Delete(string slug, CancellationToken cancellationToken)
	{
		var category = await mediator.Send(new DeleteCommand<ProductResponse>(slug), cancellationToken);

		return Ok(category);
	}
}