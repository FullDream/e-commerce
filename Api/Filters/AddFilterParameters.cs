using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Filters;

public class AddFilterParameters : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		operation.Parameters.Add(new OpenApiParameter
		{
			Name = "filter[title][contains]",
			In = ParameterLocation.Query, Description = "Filter by title containing a specific value",
			Required = false,
			Schema = new OpenApiSchema { Type = "string" }
		});

		operation.Parameters.Add(new OpenApiParameter
		{
			Name = "filter[status][equals]",
			In = ParameterLocation.Query,
			Description = "Filter by exact status",
			Required = false,
			Schema = new OpenApiSchema { Type = "string" }
		});
	}
}