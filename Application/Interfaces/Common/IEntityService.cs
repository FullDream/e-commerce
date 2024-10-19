namespace Application.Interfaces.Common;

public interface IEntityService<T, in TC, in TU>
{
	Task<List<T>> FindAllAsync();
	Task<T> FindBySlugAsync(string slug);
	Task<T> CreateAsync(TC createDto);
	Task<T> UpdateAsync(string slug, TU updateDto);
	Task<T> DeleteAsync(T product);
}