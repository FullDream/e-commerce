namespace Core.Entities;

public class Category
{
	public Guid Id { get; init; }
	public required string Name { get; init; }
	public required string Slug { get; init; }
	public required string Description { get; init; }
	public required string Icon { get; init; }

	public ICollection<Product> Products { get; set; } = new List<Product>();
}