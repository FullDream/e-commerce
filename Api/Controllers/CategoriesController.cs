using Application.Dto;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController(ICategoryService service) : ControllerBase
{
	[HttpGet]
	public Task<List<CategoryDto>> Index()
	{
		return service.FindAllAsync();
	}

	[HttpGet("{slug}")]
	public async Task<IActionResult> GetCategoryBySlug(string slug)
	{
		var category = await service.FindBySlugAsync(slug);
		return Ok(category);
	}

	[HttpPost]
	public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
	{
		var category = await service.CreateAsync(categoryDto);
		return CreatedAtAction(nameof(CreateCategory), new { id = category.Id }, category);
	}

	[HttpPatch("{slug}")]
	public async Task<IActionResult> UpdateCategory(string slug, [FromBody] UpdateCategoryDto categoryDto)
	{
		var category = await service.UpdateAsync(slug, categoryDto);

		return Ok(category);
	}

	[HttpDelete("{slug}")]
	public async Task<IActionResult> DeleteCategory(string slug)
	{
		var category = await service.DeleteAsync(slug);

		return Ok(category);
	}
}