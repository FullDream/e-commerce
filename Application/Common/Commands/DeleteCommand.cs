using MediatR;

namespace Application.Common.Commands;

public record DeleteCommand<TResult>(string Slug) : IRequest<TResult>;