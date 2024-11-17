using System.Linq.Expressions;
using Application.Common.Enums;

namespace Application.Common.Extensions;

public static class SortFieldsExtension
{
	public static IQueryable<TSource> SorFields<TSource>(this IQueryable<TSource> source,
		Dictionary<string, SortOrder> sortingCriteria)
	{
		if (sortingCriteria.Count == 0) return source;

		var parameter = Expression.Parameter(typeof(TSource), "x");

		Expression? resultExpression = null;

		foreach (var (key, sortOrder) in sortingCriteria)
		{
			var property = Expression.Property(parameter, key);
			var lambda = Expression.Lambda(property, parameter);

			var methodName = (resultExpression, sortOrder) switch
			{
				(null, SortOrder.Asc) => "OrderBy",
				(null, SortOrder.Desc) => "OrderByDescending",
				(_, SortOrder.Asc) => "ThenBy",
				(_, SortOrder.Desc) => "ThenByDescending",
			};


			resultExpression = Expression.Call(typeof(Queryable), methodName,
				[typeof(TSource), property.Type],
				resultExpression ?? source.Expression,
				Expression.Quote(lambda));
		}

		return source.Provider.CreateQuery<TSource>(resultExpression!);
	}
}