using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ApiExceptionFilter : ExceptionFilterAttribute
{
	public override void OnException(ExceptionContext context)
	{
		if (context.Exception is EntityNotFoundException)
		{
			context.Result = new NotFoundObjectResult(new
			{
				Message = context.Exception.Message
			});
		}
		else
		{
			context.Result = new ObjectResult(new
			{
				Message = "An unexpected error occurred.",
				Error = context.Exception.Message
			})
			{
				StatusCode = StatusCodes.Status500InternalServerError
			};
		}
	}
}