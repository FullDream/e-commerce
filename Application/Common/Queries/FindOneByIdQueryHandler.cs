using AutoMapper;
using Core.Interfaces;
using MediatR;

namespace Application.Common.Queries;

internal class FindOneByIdQueryHandler<TEntity, TResult>(IRepository<TEntity> repository, IMapper mapper)
	: IRequestHandler<FindOneByIdQuery<TResult>, TResult>
	where TEntity : class, IEntity
{
	public async Task<TResult> Handle(FindOneByIdQuery<TResult> request, CancellationToken cancellationToken)
	{
		var entity = await repository.FindAsync([request.Id], cancellationToken);

		return mapper.Map<TResult>(entity);
	}
}