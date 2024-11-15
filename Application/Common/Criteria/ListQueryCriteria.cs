using Application.Common.Enums;

namespace Application.Common.Criteria;

public class ListQueryCriteria : QueryCriteria
{
	public required Dictionary<string, SortOrder> Sort { get; init; }
}