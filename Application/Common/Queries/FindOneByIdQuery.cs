using MediatR;

namespace Application.Common.Queries;

public record FindOneByIdQuery<TResult>(Guid Id) : IRequest<TResult>;