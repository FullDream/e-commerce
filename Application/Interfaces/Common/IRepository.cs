namespace Application.Interfaces.Common;

public interface IRepository<TEntity> where TEntity : class
{
	Task<List<TEntity>> FindAllAsync();
	Task<List<TEntity>> FindByIdsAsync(IEnumerable<Guid> ids);
	Task<TEntity?> FindBySlugAsync(string slug);
	Task<TEntity> CreateAsync(TEntity entity);
	Task<TEntity> UpdateAsync(TEntity entity);
	Task<TEntity> DeleteAsync(TEntity entity);
}