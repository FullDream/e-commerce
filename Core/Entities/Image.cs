using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Image
{
	public Guid Id { get; init; }
	public required string Src { get; init; }
	public int Width { get; init; }
	public int Height { get; init; }
	[MaxLength(500)]
	public string Alt { get; init; } = string.Empty;
}