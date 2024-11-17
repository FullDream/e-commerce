using Api.QueryParams;
using Contracts.Common.Criteria;

namespace Api.Mapping;

public interface IQueryOptionsMapper<T>
{
	QueryCriteria Map(QueryOptions<T> options);
	ListQueryCriteria Map(ListQueryOptions<T> options);
}