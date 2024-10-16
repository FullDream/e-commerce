namespace Core.Entities;

public class Product
{
	public Guid Id { get; init; }
	public string Slug { get; init; }
	public string Title { get; init; }
	public string Description { get; init; }

	public ICollection<Image> Images { get; init; } = new List<Image>();
	public ICollection<Category> Categories { get; init; } = new List<Category>();

	public Product(string slug, string title, string description)
	{
		this.Slug = slug;
		this.Title = title;
		this.Description = description;
	}
}