using System.Linq.Expressions;
using Contracts.Common.Criteria;
using Contracts.Common.Enums;

namespace Application.Common.Extensions;

public static class FilterExtension
{
	public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, List<FilterCondition> filters)
	{
		if (filters.Count == 0)
			return query;

		var parameter = Expression.Parameter(typeof(T), "x");
		var combined = filters.Select(filter => BuildExpression<T>(filter, parameter)).OfType<Expression>()
			.Aggregate<Expression?, Expression?>(null,
				(current, expression) => current == null ? expression : Expression.AndAlso(current, expression));

		if (combined == null)
			return query;

		var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
		return query.Where(lambda);
	}

	private static Expression? BuildExpression<T>(FilterCondition filter, ParameterExpression parameter)
	{
		var property = typeof(T).GetProperty(filter.Property);
		if (property == null)
			return null;

		var propertyType = property.PropertyType;
		var underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

		Expression left = Expression.Property(parameter, property);
		if (underlyingType != propertyType)
			left = Expression.Convert(left, underlyingType);

		if (!TryConvert(filter.Value, underlyingType, out var convertedValue))
			return null;

		var right = Expression.Constant(convertedValue, underlyingType);
		var stringType = typeof(string);

		if (underlyingType == stringType)
		{
			var method = filter.Operator switch
			{
				FilterOperator.Contains => stringType.GetMethod(nameof(string.Contains), [stringType]),
				FilterOperator.StartsWith => stringType.GetMethod(nameof(string.StartsWith),
					[stringType]),
				FilterOperator.EndsWith => stringType.GetMethod(nameof(string.EndsWith), [stringType]),
				_ => null
			};

			if (method != null)
				return Expression.Call(left, method, right);
		}
		else
		{
			return filter.Operator switch
			{
				FilterOperator.Equals => Expression.Equal(left, right),
				FilterOperator.NotEquals => Expression.NotEqual(left, right),
				FilterOperator.GreaterThan => Expression.GreaterThan(left, right),
				FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
				FilterOperator.LessThan => Expression.LessThan(left, right),
				FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(left, right),
				_ => null
			};
		}

		return null;
	}

	private static bool TryConvert(object value, Type targetType, out object? result)
	{
		try
		{
			result = Convert.ChangeType(value, targetType);
			return true;
		}
		catch
		{
			result = null;
			return false;
		}
	}
}