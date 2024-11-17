using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Filters;

public class RemoveFiltersParameterOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		if (operation.Parameters == null)
			return;

		var filtersParameter = operation.Parameters.SingleOrDefault(p => p.Name == "filter");
		if (filtersParameter != null)
		{
			operation.Parameters.Remove(filtersParameter);
		}
	}
}