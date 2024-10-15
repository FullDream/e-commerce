using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductService(AppDbContext context) : IProductService
{
	public async Task<Product> CreateAsync(Product product)
	{
		// Добавляем продукт в базу данных
		context.Products.Add(product);

		// Сохраняем изменения в базе данных
		await context.SaveChangesAsync();

		// Возвращаем добавленный продукт
		return product;
	}

	public Task<Product> UpdateAsync(Product product)
	{
		throw new NotImplementedException();
	}

	public Task<List<Product>> GetAllAsync()
	{
		return context.Products.ToListAsync();
	}
}
