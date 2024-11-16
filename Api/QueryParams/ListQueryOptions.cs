using Microsoft.AspNetCore.Mvc;

namespace Api.QueryParams;

public class ListQueryOptions<T> : QueryOptions<T>
{
	[FromQuery(Name = "sort")]
	public IEnumerable<string> Sort { get; init; } = new List<string>();
}