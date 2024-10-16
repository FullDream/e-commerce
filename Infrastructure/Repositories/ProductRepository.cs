using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
	public Task<List<Product>> FindAllAsync()
	{
		return context.Products.ToListAsync();
	}

	public Task<List<Product>> FindByIdsAsync(List<Guid> ids)
	{
		return context.Products.Where(p => ids.Contains(p.Id)).ToListAsync();
	}


	public Task<Product?> FindBySlugAsync(string slug)
	{
		return context.Products.FirstOrDefaultAsync(p => p.Slug == slug);
	}

	public async Task<Product> CreateAsync(Product entity)
	{
		context.Products.Add(entity);

		await context.SaveChangesAsync();

		return entity;
	}

	public async Task<Product> UpdateAsync(Product entity)
	{
		context.Products.Update(entity);

		await context.SaveChangesAsync();

		return entity;
	}

	public async Task<Product> DeleteAsync(Product entity)
	{
		context.Products.Remove(entity);

		await context.SaveChangesAsync();

		return entity;
	}
}