using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application;

public interface IApplicationDbContext
{
	public DbSet<Core.Entities.Product> Products { get; init; }
	public DbSet<Core.Entities.Category> Categories { get; init; }
	public DbSet<Image> Images { get; init; }

	DbSet<T> Set<T>() where T : class;

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}