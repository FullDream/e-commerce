﻿namespace Application.Dto;

public class CategoryDto
{
	public Guid Id { get; init; }
	public string Name { get; init; } = "";
	public string Slug { get; init; } = "";
	public string Description { get; init; } = "";
	public string Icon { get; init; } = "";
	public IEnumerable<Guid> Products { get; init; } = [];
}