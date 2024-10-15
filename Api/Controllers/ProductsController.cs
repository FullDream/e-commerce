using Application.Interfaces;
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
		return service.GetAllAsync();
	}

	[HttpPost]
	public async Task<IActionResult> CreateProduct([FromBody] Product product)
	{
		var createdProduct = await service.CreateAsync(product);
		return CreatedAtAction(nameof(CreateProduct), new { id = createdProduct.Id }, createdProduct);
	}
}