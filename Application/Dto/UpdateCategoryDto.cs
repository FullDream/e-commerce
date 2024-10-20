namespace Application.Dto;

public class UpdateCategoryDto
{
	public string? Name { get; init; }
	public string? Slug { get; init; }
	public string? Description { get; init; }
	public string? Icon { get; init; }

	public IEnumerable<Guid>? Products { get; init; }
}