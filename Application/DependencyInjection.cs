using Application.Common.Commands;
using Application.Common.Queries;
using Contracts.Dto.Category;
using Contracts.Dto.Product;
using Core.Entities;
using Core.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

		services.AddQueries<Category, CategoryResponse>();
		services.AddCommands<Category, CategoryResponse, CreateCategoryRequest, UpdateCategoryRequest>();

		services.AddQueries<Product, ProductResponse>();
		services.AddCommands<Product, ProductResponse, CreateProductRequest, UpdateProductRequest>();

		return services;
	}

	private static IServiceCollection AddQueries<TEntity, TResponse>(this IServiceCollection services)
		where TEntity : class, IEntity
	{
		return services
			.AddScoped<IRequestHandler<FindAllQuery<TResponse>, List<TResponse>>,
				FindAllQueryHandler<TEntity, TResponse>>()
			.AddScoped<IRequestHandler<FindOneBySlugQuery<TResponse>, TResponse>,
				FindOneBySlugQueryHandler<TEntity, TResponse>>()
			.AddScoped<IRequestHandler<FindOneByIdQuery<TResponse>, TResponse>,
				FindOneByIdQueryHandler<TEntity, TResponse>>();
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