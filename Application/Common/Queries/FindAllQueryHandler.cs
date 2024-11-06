using AutoMapper;
using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Queries;

internal class FindAllQueryHandler<TEntity, TResult>(IApplicationDbContext dbContext, IMapper mapper)
	: IRequestHandler<FindAllQuery<TResult>, List<TResult>>
	where TEntity : class, IEntity

{
	private readonly DbSet<TEntity> dbSet = dbContext.Set<TEntity>();

	public Task<List<TResult>> Handle(FindAllQuery<TResult> request, CancellationToken cancellationToken)
	{
		return dbSet.AsNoTracking()
			.SorFields(request.Sorting)
			.Select(query => mapper.Map<TResult>(query))
			// .ProjectTo<TResult>(mapper.ConfigurationProvider)
			.ToListAsync(cancellationToken);
	}
}