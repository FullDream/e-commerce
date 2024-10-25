using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;

namespace Application.Services;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
{
	public async Task<List<CategoryDto>> FindAllAsync()
	{
		var categories = await categoryRepository.FindAllAsync();

		return mapper.Map<List<CategoryDto>>(categories);
	}

	public async Task<CategoryDto> FindBySlugAsync(string slug)
	{
		var category = await categoryRepository.FindBySlugAsync(slug);

		if (category == null)
			throw new EntityNotFoundException(nameof(Product), slug);

		return mapper.Map<CategoryDto>(category);
	}

	public async Task<CategoryDto> CreateAsync(CreateCategoryDto createDto)
	{
		var category = mapper.Map<Category>(createDto);


		var createdCategory = await categoryRepository.CreateAsync(category);

		return mapper.Map<CategoryDto>(createdCategory);
	}

	public async Task<CategoryDto> UpdateAsync(string slug, UpdateCategoryDto updateDto)
	{
		var category = await categoryRepository.FindBySlugAsync(slug)
		               ?? throw new EntityNotFoundException(nameof(Product), slug);

		mapper.Map(updateDto, category);

		var updatedCategory = await categoryRepository.UpdateAsync(category);

		return mapper.Map<CategoryDto>(updatedCategory);
	}

	public async Task<CategoryDto> DeleteAsync(string slug)
	{
		var category = await categoryRepository.FindBySlugAsync(slug);

		if (category == null) throw new EntityNotFoundException(nameof(Product), slug);

		var deletedCategory = categoryRepository.DeleteAsync(category);

		return mapper.Map<CategoryDto>(deletedCategory);
	}
}