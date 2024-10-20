using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;

namespace Application.Services;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
{
	public Task<List<Category>> FindAllAsync()
	{
		return categoryRepository.FindAllAsync();
	}

	public async Task<Category> FindBySlugAsync(string slug)
	{
		return await categoryRepository.FindBySlugAsync(slug) ??
		       throw new EntityNotFoundException(nameof(Product), slug);
	}

	public Task<Category> CreateAsync(CreateCategoryDto createDto)
	{
		var category = mapper.Map<Category>(createDto);

		return categoryRepository.CreateAsync(category);
	}

	public async Task<Category> UpdateAsync(string slug, UpdateCategoryDto updateDto)
	{
		var category = await categoryRepository.FindBySlugAsync(slug)
		               ?? throw new EntityNotFoundException(nameof(Product), slug);

		mapper.Map(updateDto, category);
		return await categoryRepository.UpdateAsync(category);
	}

	public async Task<Category> DeleteAsync(string slug)
	{
		var category = await categoryRepository.FindBySlugAsync(slug);

		if (category == null) throw new EntityNotFoundException(nameof(Product), slug);

		return await categoryRepository.DeleteAsync(category);
	}
}