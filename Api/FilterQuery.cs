using System.Reflection;
using Application.Common.Enums;

namespace Api;

public static class FilterQuery
{
	public static Dictionary<string, SortOrder> GetSort<T>(string[] sortParams)
	{
		var whiteList = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
			.Select(p => p.Name)
			.ToHashSet(StringComparer.OrdinalIgnoreCase);

		return sortParams
			.Where(value => value.Contains(':'))
			.Select(s =>
			{
				var parts = s.Split(':');
				var hasValidFormat = parts.Length == 2;
				var isOrder = Enum.TryParse(parts[1], true, out SortOrder order);
				return (hasValidFormat, isOrder, order, key: parts[0]);
			})
			.Where(result => result.hasValidFormat && result.isOrder)
			.Where(result => whiteList.Contains(result.key))
			.ToDictionary(
				result => whiteList.First(key => string.Equals(key, result.key, StringComparison.OrdinalIgnoreCase)),
				result => result.order);
	}
}