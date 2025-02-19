﻿using Api.QueryParams;
using Contracts.Common.Criteria;
using Contracts.Common.Enums;

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
		Sort = ParseSort(options.Sort, inspector.SimplePropertyNames),
		Filters = ParseFilter(options.Filters, inspector.SimplePropertyNames),
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
				parts => Enum.Parse<SortOrder>(parts[1], ignoreCase: true));


	private static List<FilterCondition> ParseFilter(Dictionary<string, Dictionary<string, string>> filters,
		IEnumerable<string> targetProperties)
	{
		return filters
			.SelectMany(filter =>
				filter.Value.Select(nestedDictionary => new FilterCondition
				{
					Property = targetProperties.First(t => t.Equals(filter.Key, StringComparison.OrdinalIgnoreCase)),
					Value = nestedDictionary.Value,
					Operator = Enum.Parse<FilterOperator>(nestedDictionary.Key, ignoreCase: true)
				})
			)
			.ToList();
	}
}