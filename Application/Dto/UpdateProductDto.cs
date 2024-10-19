namespace Application.Dto;

public class UpdateProductDto
{
	public string? Slug { get; init; }
	public string? Title { get; init; }
	public string? Description { get; init; }

	public ICollection<Guid>? Images { get; init; }
	public ICollection<Guid>? Categories { get; init; }
}