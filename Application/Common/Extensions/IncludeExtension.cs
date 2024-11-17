using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extensions;

public static class IncludeExtension
{
	public static IQueryable<T> IncludeMany<T>(
		this IQueryable<T> source,
		IEnumerable<string> navigationProperties) where T : class =>
		navigationProperties.Aggregate(source, (current, navProperty) => current.Include(navProperty));
}