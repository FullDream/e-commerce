using System.ComponentModel.DataAnnotations;

namespace Application.Dto;

public class CreateCategoryDto
{
	[Required]
	public  string Name { get; init; }= null!;

	[Required]
	public  string Slug { get; init; }= null!;

	[Required]
	public  string Description { get; init; } = null!;

	[Required]
	public  string Icon { get; init; }= null!;

	public ICollection<Guid>? Products { get; init; } = new List<Guid>();
}