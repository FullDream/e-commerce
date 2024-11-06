using MediatR;

namespace Application.Common.Queries;

public record FindAllQuery<TResult>(List<string> Select) : IRequest<List<TResult>>;