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

	public IEnumerable<Guid>? Images { get; init; }
	public IEnumerable<Guid>? Categories { get; init; }
}