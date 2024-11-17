using Api.QueryParams;
using Application.Common.Enums;
using FluentValidation;

namespace Api.Validators;

public class QueryOptionsValidator<T> : AbstractValidator<QueryOptions<T>>, IQueryOptionsValidator<T>
{
	public QueryOptionsValidator(TypeInspector<T> typeInspector)
	{
		var validSelectFields =
			new HashSet<string>(typeInspector.SimplePropertyNames, StringComparer.OrdinalIgnoreCase);
		var validIncludeFields =
			new HashSet<string>(typeInspector.NavigationPropertyNames, StringComparer.OrdinalIgnoreCase);

		RuleForEach(q => q.Select)
			.Must(field => validSelectFields.Contains(field));

		RuleForEach(q => q.Include)
			.Must(field => validIncludeFields.Contains(field));

		When(q => q is ListQueryOptions<T>, () =>
			{
				RuleForEach(q => ((ListQueryOptions<T>)q).Sort)
					.Custom((field, context) =>
					{
						var parts = field.Split(':');
						if (parts.Length != 2)
						{
							context.AddFailure(
								"Invalid format for sort field. Expected format is 'Field:Order' (e.g., 'Name:asc').");
							return;
						}

						var propertyName = parts[0];
						var sortOrder = parts[1];

						if (!validSelectFields.Contains(propertyName))
							context.AddFailure($"The field '{propertyName}' is not supported for sorting.");


						if (!Enum.TryParse<SortOrder>(sortOrder, true, out _))
							context.AddFailure($"The sort order '{sortOrder}' is invalid. Expected 'asc' or 'desc'.");
					});


				RuleForEach(q => ((ListQueryOptions<T>)q).Filters)
					.ChildRules(filter =>
					{
						filter.RuleFor(f => f.Key)
							.Must(key => validSelectFields.Contains(key))
							.WithMessage(f => $"The filter property '{f.Key}' is invalid.");

						filter.RuleFor(f => f.Value)
							.NotNull()
							.WithMessage("Filter conditions cannot be null.");

						filter.RuleForEach(f => f.Value)
							.ChildRules(nested =>
							{
								nested.RuleFor(n => n.Key)
									.Must(op => Enum.TryParse<FilterOperator>(op, ignoreCase: true, out _))
									.WithMessage(n => $"The filter operator '{n.Key}' is invalid.");

								nested.RuleFor(n => n.Value)
									.NotEmpty()
									.WithMessage("The filter value cannot be empty.");
							});
					});
			}
		);
	}
}