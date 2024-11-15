using Api.QueryParams;
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
	}
}