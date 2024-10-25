using Core;

namespace Application.Interfaces.Common;

public interface IRepository<TEntity> where TEntity : IEntity
{
	Task<List<TEntity>> FindAllAsync();
	Task<List<TEntity>> FindByIdsAsync(IEnumerable<Guid> ids);
	ValueTask<TEntity?> FindAsync(Guid id);
	Task<TEntity?> FindAsync(string slug);
	Task<TEntity> CreateAsync(TEntity entity);
	Task<TEntity> UpdateAsync(TEntity entity);
	Task<TEntity> DeleteAsync(TEntity entity);
}