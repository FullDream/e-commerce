using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
	public static IServiceCollection AddDbInfrastructure(this IServiceCollection services,
		IConfiguration configuration)
	{

		services.AddDbContext<AppDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

		return services;
	}

	public static IServiceCollection AddMappingInfrastructure(this IServiceCollection services)
	{
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		return services;
	}
}