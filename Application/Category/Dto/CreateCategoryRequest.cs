﻿using System.ComponentModel.DataAnnotations;

namespace Application.Category.Dto;

public class CreateCategoryRequest
{
	[Required]
	public string Name { get; init; } = null!;

	[Required]
	public string Slug { get; init; } = null!;

	[Required]
	public string Description { get; init; } = null!;

	[Required]
	public string Icon { get; init; } = null!;

	public IReadOnlyCollection<Guid>? Products { get; init; }
}