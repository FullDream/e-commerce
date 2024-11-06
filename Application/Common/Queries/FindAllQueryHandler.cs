using AutoMapper;
using AutoMapper.QueryableExtensions;
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
			.ProjectTo<TResult>(mapper.ConfigurationProvider)
			.ToListAsync(cancellationToken);
	}


	public static IQueryable<T> IncludeFirstLevelNavigationProperties<T>(
		DbContext context,
		IQueryable<T> query,
		IEnumerable<string> propertyNames) where T : class
	{
		var entityType = context.Model.FindEntityType(typeof(T));

		foreach (var propertyName in propertyNames)
		{
			var navigation = entityType.FindNavigation(propertyName);

			if (navigation != null)
			{
				// Свойство является навигационным
				query = query.Include(propertyName);
			}
			else
			{
				// Свойство не является навигационным или не существует
				// Можно выбросить исключение или просто пропустить
				throw new ArgumentException(
					$"Свойство '{propertyName}' не является навигационным в сущности '{entityType.Name}'.");
			}
		}

		return query;
	}
}