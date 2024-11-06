using AutoMapper;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;

namespace Application.Common.Commands;

internal class CreateCommandHandler<TEntity, TDto, TResult>(IRepository<TEntity> repository, IMapper mapper)
	: IRequestHandler<CreateCommand<TDto, TResult>, TResult>
	where TEntity : class, IEntity
{
	public async Task<TResult> Handle(CreateCommand<TDto, TResult> request, CancellationToken cancellationToken)
	{
		var category = await repository.FindAsync(request.Slug, cancellationToken)
		               ?? throw new EntityNotFoundException(nameof(TEntity), request.Slug);

		mapper.Map(request.Dto, category);

		var updatedCategory = await repository.UpdateAsync(category, cancellationToken);

		return mapper.Map<TResult>(updatedCategory);
	}
}