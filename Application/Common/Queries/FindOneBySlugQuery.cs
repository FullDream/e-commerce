using Contracts.Common.Criteria;
using MediatR;

namespace Application.Common.Queries;

public record FindOneBySlugQuery<TResult>(string Slug, QueryCriteria Criteria) : IRequest<TResult>;