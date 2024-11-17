namespace Application.Category.Dto;

public class BaseCategory
{
	public required Guid Id { get; init; }
	public string? Name { get; init; }
	public string? Slug { get; init; }
	public string? Description { get; init; }
	public string? Icon { get; init; }
}