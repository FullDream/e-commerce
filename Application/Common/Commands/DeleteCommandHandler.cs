using Application.Interfaces.Common;
using AutoMapper;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;

namespace Application.Common.Commands;

internal class DeleteCommandHandler<TEntity, TResult>(IRepository<TEntity> repository, IMapper mapper)
	: IRequestHandler<DeleteCommand<TResult>, TResult>
	where TEntity : class, IEntity
{
	public async Task<TResult> Handle(DeleteCommand<TResult> request, CancellationToken cancellationToken)
	{
		var entity = await repository.FindAsync(request.Slug, cancellationToken);

		if (entity == null) throw new EntityNotFoundException(nameof(TEntity), request.Slug);

		var deletedEntity = repository.DeleteAsync(entity, cancellationToken);

		return mapper.Map<TResult>(deletedEntity);
	}
}