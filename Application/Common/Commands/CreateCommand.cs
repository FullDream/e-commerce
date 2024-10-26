using MediatR;

namespace Application.Common.Commands;

public record CreateCommand<TDto, TResult>(string Slug, TDto Dto) : IRequest<TResult>;