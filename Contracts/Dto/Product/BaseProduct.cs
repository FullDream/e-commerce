using Core.Entities;

namespace Contracts.Dto.Product;

public class BaseProduct
{
	public Guid Id { get; init; }
	public string? Slug { get; init; }
	public string? Title { get; init; }
	public string? Description { get; init; }

	public IReadOnlyCollection<Image>? Images { get; init; }
}