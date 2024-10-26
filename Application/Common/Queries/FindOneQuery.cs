using MediatR;

namespace Application.Common.Queries;

public record FindOneQuery<TResult>(string Slug) : IRequest<TResult>;