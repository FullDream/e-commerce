using Api.QueryParams;
using Application.Common.Enums;
using FluentValidation;

namespace Api.Validators;

public class ListQueryOptionsValidator<T> : AbstractValidator<ListQueryOptions<T>>, IListQueryOptionsValidator<T>
{
	public ListQueryOptionsValidator(TypeInspector<T> typeInspector)
	{
		var validSelectFields =
			new HashSet<string>(typeInspector.SimplePropertyNames, StringComparer.OrdinalIgnoreCase);
		var validIncludeFields =
			new HashSet<string>(typeInspector.NavigationPropertyNames, StringComparer.OrdinalIgnoreCase);

		RuleForEach(q => q.Select)
			.Must(field => validSelectFields.Contains(field));

		RuleForEach(q => q.Include)
			.Must(field => validIncludeFields.Contains(field));

		RuleForEach(q => q.Sort)
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
	}
}