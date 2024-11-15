using Microsoft.AspNetCore.Mvc;

namespace Api.QueryParams;

public class QueryOptions<T>
{
	[FromQuery(Name = "select")]
	public IEnumerable<string> Select { get; init; } = [];

	[FromQuery(Name = "include")]
	public IEnumerable<string> Include { get; init; } = [];
}