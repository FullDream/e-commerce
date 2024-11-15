using Application.Common.Criteria;
using MediatR;

namespace Application.Common.Queries;

public record FindAllQuery<TResult>(ListQueryCriteria Criteria)
	: IRequest<List<TResult>>;