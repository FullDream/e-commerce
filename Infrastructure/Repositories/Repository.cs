using Application.Interfaces.Common;
using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity>(DbContext context) : IRepository<TEntity> where TEntity : class, IEntity
{
	private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

	public Task<List<TEntity>> FindAllAsync()
	{
		return dbSet.AsNoTracking().ToListAsync();
	}

	public Task<List<TEntity>> FindByIdsAsync(IEnumerable<Guid> ids)
	{
		return dbSet.Where(entity => ids.Contains(entity.Id)).ToListAsync();
	}

	public ValueTask<TEntity?> FindAsync(Guid id)
	{
		return dbSet.FindAsync(id);
	}

	public Task<TEntity?> FindAsync(string slug)
	{
		return dbSet.FirstOrDefaultAsync(entity => entity.Slug == slug);
	}

	public async Task<TEntity> CreateAsync(TEntity entity)
	{
		dbSet.Add(entity);

		await context.SaveChangesAsync();

		return entity;
	}

	public async Task<TEntity> UpdateAsync(TEntity entity)
	{
		dbSet.Update(entity);

		await context.SaveChangesAsync();

		return entity;
	}

	public async Task<TEntity> DeleteAsync(TEntity entity)
	{
		dbSet.Remove(entity);

		await context.SaveChangesAsync();

		return entity;
	}
}