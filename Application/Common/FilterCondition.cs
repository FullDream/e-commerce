using Application.Common.Enums;

namespace Application.Common;

public class FilterCondition
{
	public required string Property { get; init; }
	public required FilterOperator Operator { get; init; }
	public required object Value { get; init; }
}