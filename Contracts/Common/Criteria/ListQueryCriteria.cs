using Contracts.Common.Enums;

namespace Contracts.Common.Criteria;

public class ListQueryCriteria : QueryCriteria
{
	public required Dictionary<string, SortOrder> Sort { get; init; }
	public required List<FilterCondition> Filters { get; init; }
}