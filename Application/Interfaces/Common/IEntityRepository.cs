namespace Application.Interfaces.Common;

public interface IEntityRepository<T>
{
	Task<List<T>> FindAllAsync();

	Task<List<T>> FindByIdsAsync( List<Guid> ids);
	Task<T?> FindBySlugAsync(string slug);
	Task<T> CreateAsync(T entity);
	Task<T> UpdateAsync( T entity);
	Task<T> DeleteAsync(T entity);
}