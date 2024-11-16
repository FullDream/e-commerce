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
public class ProductsController(
	IMediator mediator,
	IQueryOptionsValidator<ProductResponse> queryValidator,
	IQueryOptionsMapper<ProductResponse> queryMapper
) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult>
		Index(
			ListQueryOptions<ProductResponse> options,
			CancellationToken cancellationToken
		)
	{
		var validation = await queryValidator.ValidateAsync(options, cancellationToken);

		if (!validation.IsValid) return BadRequest(validation);

		var criteria = queryMapper.Map(options);
		var categories = await mediator.Send(new FindAllQuery<ProductResponse>(criteria), cancellationToken);

		return Ok(categories);
	}

	[HttpGet("{slug}")]
	public async Task<IActionResult> FindOne(
		string slug,
		[FromQuery] QueryOptions<ProductResponse> options,
		CancellationToken cancellationToken
	)
	{
		var validationResult = await queryValidator.ValidateAsync(options, cancellationToken);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors);

		QueryCriteria criteria = queryMapper.Map(options);
		var product = await mediator.Send(new FindOneBySlugQuery<ProductResponse>(slug, criteria), cancellationToken);
		return Ok(product);
	}

	[HttpGet("{id:guid}")]
	public async Task<IActionResult> FindOne(
		Guid id,
		[FromQuery] QueryOptions<ProductResponse> options,
		CancellationToken cancellationToken
	)
	{
		QueryCriteria criteria = queryMapper.Map(options);
		var product = await mediator.Send(new FindOneByIdQuery<ProductResponse>(id, criteria), cancellationToken);
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