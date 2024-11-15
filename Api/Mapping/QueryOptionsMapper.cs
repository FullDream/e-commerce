using Api.QueryParams;
using Application.Common.Criteria;
using Application.Common.Enums;

namespace Api.Mapping;

public class QueryOptionsMapper<T>(TypeInspector<T> inspector) : IQueryOptionsMapper<T>
{
	public QueryCriteria Map(QueryOptions<T> options) => new()
	{
		Select = ResolveProperties(options.Select, inspector.SimplePropertyNames),
		Include = ResolveProperties(options.Include, inspector.NavigationPropertyNames),
	};


	public ListQueryCriteria Map(ListQueryOptions<T> options) => new()
	{
		Select = ResolveProperties(options.Select, inspector.SimplePropertyNames),
		Include = ResolveProperties(options.Include, inspector.NavigationPropertyNames),
		Sort = ParseSort(options.Sort, inspector.SimplePropertyNames)
	};


	private static IEnumerable<string> ResolveProperties(IEnumerable<string> source, IEnumerable<string> target) =>
		source
			.Select(s => target.FirstOrDefault(t => t.Equals(s, StringComparison.OrdinalIgnoreCase)))
			.OfType<string>();


	private static Dictionary<string, SortOrder> ParseSort(
		IEnumerable<string> sortParams,
		IEnumerable<string> target
	) =>
		sortParams
			.Select(param => param.Split(':'))
			.ToDictionary(parts => target.First(k => k.Equals(parts[0], StringComparison.OrdinalIgnoreCase)),
				parts => Enum.Parse<SortOrder>(parts[1]));
}