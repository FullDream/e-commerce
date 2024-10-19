using System.ComponentModel.DataAnnotations;

namespace Application.Dto;

public class CreateProductDto
{
	[Required]
	public string Slug { get; init; } = null!;

	[Required]
	public string Title { get; init; } = null!;

	[Required]
	public string Description { get; init; } = null!;

	public ICollection<Guid>? Images { get; init; }
	public ICollection<Guid>? Categories { get; init; }
}