using AutoMapper;
using Core.Interfaces;
using MediatR;

namespace Application.Common.Queries;

internal class FindOneBySlugQueryHandler<TEntity, TResult>(IRepository<TEntity> repository, IMapper mapper)
	: IRequestHandler<FindOneBySlugQuery<TResult>, TResult>
	where TEntity : class, IEntity
{
	public async Task<TResult> Handle(FindOneBySlugQuery<TResult> request, CancellationToken cancellationToken)
	{
		var entity = await repository.FindAsync(request.Slug, cancellationToken);

		return mapper.Map<TResult>(entity);
	}
}