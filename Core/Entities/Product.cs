namespace Core.Entities;

public class Product
{
	public Guid Id { get; init; }
	public required string Slug { get; init; }
	public required string Title { get; init; }
	public required string Description { get; init; }

	public ICollection<Image> Images { get; init; } = new List<Image>();
	public ICollection<Category> Categories { get; init; } = new List<Category>();
}