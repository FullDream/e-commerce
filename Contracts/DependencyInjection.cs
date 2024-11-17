using Microsoft.Extensions.DependencyInjection;

namespace Contracts;

public static class DependencyInjection
{
	public static IServiceCollection AddContracts(this IServiceCollection services)
	{
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		return services;
	}
}