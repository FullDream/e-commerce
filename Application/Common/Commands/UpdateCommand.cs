using MediatR;

namespace Application.Common.Commands;

public record UpdateCommand<TDto, TResult>(TDto Dto) : IRequest<TResult>;