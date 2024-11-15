using Application.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Api.QueryParams;

public class ListQueryOptions<T> : QueryOptions<T>
{
	[FromQuery(Name = "sort")]
	public IEnumerable<string> Sort { get; init; } = [];

	public static Dictionary<string, SortOrder> GetSort(string[] sortParams, string[] validProperties)
	{
		if (sortParams.Length == 0) return [];

		var whiteList = validProperties.ToHashSet(StringComparer.OrdinalIgnoreCase);

		return sortParams
			.Select(param => param.Split(':'))
			.Where(parts => parts.Length == 2 && whiteList.Contains(parts[0]))
			.Select(parts => new
			{
				Key = whiteList.First(k => string.Equals(k, parts[0], StringComparison.OrdinalIgnoreCase)),
				IsParsed = Enum.TryParse(parts[1], true, out SortOrder order),
				Order = order
			})
			.Where(x => x.IsParsed)
			.ToDictionary(x => x.Key, x => x.Order);
	}

	public static string[] GetInclude(string[] includeParams, string[] dtoPropertyNames)
	{
		if (includeParams.Length == 0) return includeParams;

		var dtoPropertyDict = dtoPropertyNames
			.ToDictionary(name => name, StringComparer.OrdinalIgnoreCase);

		return includeParams
			.Where(param => dtoPropertyDict.ContainsKey(param))
			.Select(param => dtoPropertyDict[param])
			.ToArray();
	}
}