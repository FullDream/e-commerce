using Application.Common.Commands;
using Application.Common.Queries;
using Application.Product.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController(IMediator mediator, TypeInspector<ProductResponse> typeInspector) : ControllerBase
{
	[HttpGet]
	public async Task<IActionResult> Index([FromQuery] string[] select, [FromQuery] string[] include,
		[FromQuery] string[] sort,
		CancellationToken cancellationToken)
	{
		var sorting = FilterQuery.GetSort<ProductResponse>(sort);
		var products =
			await mediator.Send(new FindAllQuery<ProductResponse>(select, include, sorting), cancellationToken);
		return Ok(products);
	}

	[HttpGet("{slug}")]
	public async Task<IActionResult> FindOne(string slug, CancellationToken cancellationToken)
	{
		var product = await mediator.Send(new FindOneQuery<ProductResponse>(slug), cancellationToken);
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