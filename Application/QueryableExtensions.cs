using System.Linq.Expressions;
using System.Reflection;
using Application.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application;

public static class QueryableExtensions
{
	public static IQueryable<TDestination> SelectFields<TSource, TDestination>(this IQueryable<TSource> source,
		List<string> fields)
	{
		var parameter = Expression.Parameter(typeof(TSource), "x");
		var bindings = new List<MemberBinding>();

		foreach (var field in fields)
		{
			var sourceProperty = typeof(TSource).GetProperty(field,
				BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
			var targetProperty = typeof(TDestination).GetProperty(field,
				BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (sourceProperty != null && targetProperty != null)
			{
				var propertyAccess = Expression.Property(parameter, sourceProperty);
				var binding = Expression.Bind(targetProperty, propertyAccess);
				bindings.Add(binding);
			}
		}

		var body = Expression.MemberInit(Expression.New(typeof(TDestination)), bindings);
		var selector = Expression.Lambda<Func<TSource, TDestination>>(body, parameter);

		return source.Select(selector);
	}

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
				new Type[] { typeof(TSource), property.Type },
				resultExpression ?? source.Expression,
				Expression.Quote(lambda));
		}

		return source.Provider.CreateQuery<TSource>(resultExpression!);
	}

	public static IQueryable<T> IncludeMany<T>(this IQueryable<T> source,
		IEnumerable<string> navigationProperties) where T : class
	{
		return navigationProperties.Aggregate(source, (current, navProperty) => current.Include(navProperty));
	}
}