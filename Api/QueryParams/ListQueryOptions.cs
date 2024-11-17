using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Api.QueryParams;

public class ListQueryOptions<T> : QueryOptions<T>
{
	[FromQuery(Name = "sort")]
	public IEnumerable<string> Sort { get; init; } = [];

	[JsonIgnore]
	[FromQuery(Name = "filter")]
	public Dictionary<string, Dictionary<string, string>> Filters { get; init; } = new();
}