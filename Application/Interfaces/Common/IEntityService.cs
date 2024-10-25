namespace Application.Interfaces.Common;

public interface IEntityService<TDto, in TC, in TU>
{
	Task<List<TDto>> FindAllAsync();
	Task<TDto> FindBySlugAsync(string slug);
	Task<TDto> CreateAsync(TC createDto);
	Task<TDto> UpdateAsync(string slug, TU updateDto);
	Task<TDto> DeleteAsync(string slug);
}