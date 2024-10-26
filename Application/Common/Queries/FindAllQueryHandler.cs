using Application.Interfaces.Common;
using AutoMapper;
using Core.Interfaces;
using MediatR;

namespace Application.Common.Queries;

internal class FindAllQueryHandler<TEntity, TResult>(IRepository<TEntity> repository, IMapper mapper)
	: IRequestHandler<FindAllQuery<TResult>, List<TResult>>
	where TEntity : class, IEntity

{
	public async Task<List<TResult>> Handle(FindAllQuery<TResult> request, CancellationToken cancellationToken)
	{
		var entities = await repository.FindAllAsync(cancellationToken);
		return mapper.Map<List<TResult>>(entities);
	}
}