using Contracts.Common.Enums;

namespace Contracts.Common.Criteria;

public class FilterCondition
{
	public required string Property { get; init; }
	public required FilterOperator Operator { get; init; }
	public required object Value { get; init; }
}