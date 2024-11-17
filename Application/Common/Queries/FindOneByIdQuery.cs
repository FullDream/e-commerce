using Contracts.Common.Criteria;
using MediatR;

namespace Application.Common.Queries;

public record FindOneByIdQuery<TResult>(Guid Id, QueryCriteria Criteria) : IRequest<TResult>;