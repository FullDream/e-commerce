namespace Core.Interfaces;

public interface IRepository<TEntity> where TEntity : IEntity
{
	Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken);
	Task<List<TEntity>> FindByIdsAsync(IEnumerable<Guid> ids);
	ValueTask<TEntity?> FindAsync(object[] id, CancellationToken cancellationToken);
	Task<TEntity?> FindAsync(string slug, CancellationToken cancellationToken);
	Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
	Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
	Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}