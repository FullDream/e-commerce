namespace Core.Interfaces;

public interface IEntity
{
	public Guid Id { get; init; }
	public string Slug { get; init; }
}