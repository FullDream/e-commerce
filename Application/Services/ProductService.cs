using Application.Dto;
using Application.Interfaces;
using Application.Interfaces.Services;
using Core.Entities;
using Core.Exceptions;

namespace Application.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
	public Task<List<Product>> FindAllAsync()
	{
		return productRepository.FindAllAsync();
	}

	public async Task<Product> FindBySlugAsync(string slug)
	{
		var product = await productRepository.FindBySlugAsync(slug);

		if (product != null) return product;

		throw new EntityNotFoundException(nameof(Product), slug);
	}

	public Task<Product> CreateAsync(CreateProductDto createDto)
	{
		Product product = new(createDto.Slug, createDto.Title, createDto.Description);


		return productRepository.CreateAsync(product);
	}

	public Task<Product> UpdateAsync(Product product)
	{
		return productRepository.UpdateAsync(product);
	}

	public Task<Product> DeleteAsync(Product product)
	{
		return productRepository.DeleteAsync(product);
	}
}