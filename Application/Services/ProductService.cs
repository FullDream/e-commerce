using Application.Dto;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;

namespace Application.Services;

public class ProductService(
	IMapper mapper,
	IProductRepository productRepository,
	ICategoryRepository categoryRepository) : IProductService
{
	public async Task<List<ProductDto>> FindAllAsync()
	{
		var products = await productRepository.FindAllAsync();

		return mapper.Map<List<ProductDto>>(products);
	}

	public async Task<ProductDto> FindBySlugAsync(string slug)
	{
		var product = await productRepository.FindBySlugAsync(slug);

		if (product == null)
			throw new EntityNotFoundException(nameof(Product), slug);

		return mapper.Map<ProductDto>(product);
	}

	public async Task<ProductDto> CreateAsync(CreateProductDto createDto)
	{
		var product = mapper.Map<Product>(createDto);

		var createdProduct = await productRepository.CreateAsync(product);

		return mapper.Map<ProductDto>(createdProduct);
	}

	public async Task<ProductDto> UpdateAsync(string slug, UpdateProductDto updateDto)
	{
		var product = await productRepository.FindBySlugAsync(slug)
		              ?? throw new EntityNotFoundException(nameof(Product), slug);

		mapper.Map(updateDto, product);

		if (updateDto.Categories is not null)
		{
			var categories = await categoryRepository.FindByIdsAsync(updateDto.Categories);

			if (categories.Count != updateDto.Categories.Count())
			{
				throw new EntityNotFoundException(nameof(Category), "One or more categories not found.");
			}

			product.Categories = categories.ToList();
		}

		var updatedProduct = await productRepository.UpdateAsync(product);

		return mapper.Map<ProductDto>(updatedProduct);
	}

	public async Task<ProductDto> DeleteAsync(string slug)
	{
		var product = await productRepository.FindBySlugAsync(slug);

		if (product == null) throw new EntityNotFoundException(nameof(Product), slug);

		var deletedProduct = await productRepository.DeleteAsync(product);

		return mapper.Map<ProductDto>(deletedProduct);
	}
}