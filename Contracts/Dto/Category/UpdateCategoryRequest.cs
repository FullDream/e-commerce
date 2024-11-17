namespace Contracts.Dto.Category;

public class UpdateCategoryRequest
{
	public string? Name { get; init; }
	public string? Slug { get; init; }
	public string? Description { get; init; }
	public string? Icon { get; init; }

	public IReadOnlyCollection<Guid>? Products { get; init; }
}