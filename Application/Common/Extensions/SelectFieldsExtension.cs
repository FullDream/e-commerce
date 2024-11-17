using System.Linq.Expressions;
using System.Reflection;

namespace Application.Common.Extensions;

public static class SelectFieldsExtension
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
}