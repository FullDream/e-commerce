using Application.Dto;
using Application.Interfaces;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities;
using Core.Exceptions;

namespace Application.Services;

public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
{
	public Task<List<Product>> FindAllAsync()
	{
		return productRepository.FindAllAsync();
	}

	public async Task<Product> FindBySlugAsync(string slug)
	{
		return await productRepository.FindBySlugAsync(slug) ??
		       throw new EntityNotFoundException(nameof(Product), slug);
	}

	public Task<Product> CreateAsync(CreateProductDto createDto)
	{
		var product = mapper.Map<Product>(createDto);

		return productRepository.CreateAsync(product);
	}

	public async Task<Product> UpdateAsync(string slug, UpdateProductDto updateDto)
	{
		var product = await productRepository.FindBySlugAsync(slug)
		              ?? throw new EntityNotFoundException(nameof(Product), slug);

		mapper.Map(updateDto, product);
		return await productRepository.UpdateAsync(product);
	}

	public Task<Product> DeleteAsync(Product product)
	{
		return productRepository.DeleteAsync(product);
	}
}