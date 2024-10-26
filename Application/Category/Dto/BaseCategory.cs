namespace Application.Category.Dto;

public class BaseCategory
{
	public Guid Id { get; init; }
	public string Name { get; init; } = string.Empty;
	public string Slug { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;
	public string Icon { get; init; } = string.Empty;
}