using MediatR;

namespace Application.Common.Queries;

public record FindAllQuery<TResult> : IRequest<List<TResult>>;