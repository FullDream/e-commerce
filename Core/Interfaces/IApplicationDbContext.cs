using Microsoft.EntityFrameworkCore;

namespace Core.Interfaces;

public interface IApplicationDbContext
{
	DbSet<T> Set<T>() where T : class;

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}