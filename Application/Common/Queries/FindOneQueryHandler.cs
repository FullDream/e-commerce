using Application.Interfaces.Common;
using AutoMapper;
using Core.Interfaces;
using MediatR;

namespace Application.Common.Queries;

internal class FindOneQueryHandler<TEntity, TResult>(IRepository<TEntity> repository, IMapper mapper)
	: IRequestHandler<FindOneQuery<TResult>, TResult>
	where TEntity : class, IEntity
{
	public async Task<TResult> Handle(FindOneQuery<TResult> request, CancellationToken cancellationToken)
	{
		var entity = await repository.FindAsync(request.Slug, cancellationToken);

		return mapper.Map<TResult>(entity);
	}
}