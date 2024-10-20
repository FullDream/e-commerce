using Application.Interfaces.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
	public Task<List<Category>> FindAllAsync()
	{
		return context.Categories.ToListAsync();
	}

	public Task<List<Category>> FindByIdsAsync(List<Guid> ids)
	{
		return context.Categories.Where(category => ids.Contains(category.Id)).ToListAsync();
	}


	public Task<Category?> FindBySlugAsync(string slug)
	{
		return context.Categories.FirstOrDefaultAsync(category => category.Slug == slug);
	}

	public async Task<Category> CreateAsync(Category entity)
	{
		context.Categories.Add(entity);

		await context.SaveChangesAsync();

		return entity;
	}

	public async Task<Category> UpdateAsync(Category entity)
	{
		context.Categories.Update(entity);

		await context.SaveChangesAsync();

		return entity;
	}

	public async Task<Category> DeleteAsync(Category entity)
	{
		context.Categories.Remove(entity);

		await context.SaveChangesAsync();

		return entity;
	}
}