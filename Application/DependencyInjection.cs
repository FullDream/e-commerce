using Application.Category.Dto;
using Application.Common.Commands;
using Application.Common.Queries;
using Application.Product.Dto;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

		services.AddQueries<Core.Entities.Category, CategoryResponse>();
		services.AddCommands<Core.Entities.Category, CategoryResponse, CreateCategoryRequest, UpdateCategoryRequest>();

		services.AddQueries<Core.Entities.Product, ProductResponse>();
		services.AddCommands<Core.Entities.Product, ProductResponse, CreateProductRequest, UpdateProductRequest>();

		return services;
	}

	private static IServiceCollection AddQueries<TEntity, TResponse>(this IServiceCollection services)
		where TEntity : class, IEntity
	{
		return services
			.AddScoped<IRequestHandler<FindAllQuery<TResponse>, List<TResponse>>,
				FindAllQueryHandler<TEntity, TResponse>>()
			.AddScoped<IRequestHandler<FindOneQuery<TResponse>, TResponse>,
				FindOneQueryHandler<TEntity, TResponse>>();
	}

	private static IServiceCollection AddCommands<TEntity, TResponse, TCreate, TUpdate>(
		this IServiceCollection services)
		where TEntity : class, IEntity
	{
		return services
			.AddScoped<IRequestHandler<CreateCommand<TCreate, TResponse>, TResponse>,
				CreateCommandHandler<TEntity, TCreate, TResponse>>()
			.AddScoped<IRequestHandler<UpdateCommand<TUpdate, TResponse>, TResponse>,
				UpdateCommandHandler<TEntity, TUpdate, TResponse>>()
			.AddScoped<IRequestHandler<DeleteCommand<TResponse>, TResponse>,
				DeleteCommandHandler<TEntity, TResponse>>();
	}
}