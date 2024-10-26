namespace Application.Product.Dto;

public class BaseProduct
{
	public Guid Id { get; init; }
	public string Slug { get; init; } = string.Empty;
	public string Title { get; init; } = string.Empty;
	public string Description { get; init; } = string.Empty;

	public IReadOnlyCollection<Guid> Images { get; init; } = [];
}