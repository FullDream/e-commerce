namespace Application.Interfaces.Common;

public interface IEntityService<T, in TC>
{
	Task<List<T>> FindAllAsync();
	Task<T> FindBySlugAsync(string slug);
	Task<T> CreateAsync(TC createDto);
	Task<T> UpdateAsync(T category);
	Task<T> DeleteAsync(T product);
}