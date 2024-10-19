using System.Text.Json;
using Application.Dto;
using Application.Interfaces;
using Application.Interfaces.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductsController(IProductService service) : ControllerBase
{
	[HttpGet]
	public Task<List<Product>> Index()
	{
		return service.FindAllAsync();
	}

	[HttpGet("{slug}")]
	public async Task<IActionResult> GetProductBySlug(string slug)
	{
		var product = await service.FindBySlugAsync(slug);
		return Ok(product);
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
	{
		var createdProduct = await service.CreateAsync(productDto);
		return CreatedAtAction(nameof(CreateProduct), new { id = createdProduct.Id }, createdProduct);
	}

	[HttpPatch("{slug}")]
	public async Task<IActionResult> UpdateProduct(string slug, [FromBody] UpdateProductDto productDto)
	{
		var product =  await service.UpdateAsync(slug, productDto);

		return Ok(product);
	}
}