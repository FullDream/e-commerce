namespace Core.Entities;

public class Product : IEntity
{
	public Guid Id { get; init; }
	public string Slug { get; init; }
	public string Title { get; init; }
	public string Description { get; init; }

	public ICollection<Image> Images { get; init; } = new List<Image>();
	public ICollection<Category> Categories { get; set; } = new List<Category>();
}