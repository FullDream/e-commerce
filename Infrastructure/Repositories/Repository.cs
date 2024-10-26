using Application.Interfaces.Common;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity> where TEntity : class, IEntity
{
	private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

	public Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken)
	{
		return dbSet.AsNoTracking().ToListAsync(cancellationToken);
	}

	public Task<List<TEntity>> FindByIdsAsync(IEnumerable<Guid> ids)
	{
		return dbSet.Where(entity => ids.Contains(entity.Id)).ToListAsync();
	}

	public ValueTask<TEntity?> FindAsync(object[] id, CancellationToken cancellationToken)
	{
		return dbSet.FindAsync(id, cancellationToken);
	}

	public Task<TEntity?> FindAsync(string slug, CancellationToken cancellationToken)
	{
		return dbSet.FirstOrDefaultAsync(entity => entity.Slug == slug, cancellationToken: cancellationToken);
	}

	public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
	{
		dbSet.Add(entity);

		await context.SaveChangesAsync(cancellationToken);

		return entity;
	}

	public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
	{
		dbSet.Update(entity);

		await context.SaveChangesAsync(cancellationToken);

		return entity;
	}

	public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
	{
		dbSet.Remove(entity);

		await context.SaveChangesAsync(cancellationToken);

		return entity;
	}
}