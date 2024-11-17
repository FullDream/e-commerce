namespace Contracts.Common.Criteria;

public class QueryCriteria
{
	public required IEnumerable<string> Select { get; init; }
	public required IEnumerable<string> Include { get; init; }
}