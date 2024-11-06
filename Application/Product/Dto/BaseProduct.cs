namespace Application.Product.Dto;

public class BaseProduct
{
	public Guid Id { get; init; }
	public string? Slug { get; init; }
	public string? Title { get; init; }
	public string? Description { get; init; }

	// public IReadOnlyCollection<Guid>? Images { get; init; }
}