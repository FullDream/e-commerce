using System.Text.Json;
using Api;
using Api.Filters;
using Api.Mapping;
using Api.Routing;
using Api.Validators;
using Application;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddDbInfrastructure(builder.Configuration);
services.AddMappingInfrastructure();

services.AddSingleton(typeof(TypeInspector<>));
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
services.AddScoped(typeof(IQueryOptionsValidator<>), typeof(QueryOptionsValidator<>));
services.AddScoped(typeof(IQueryOptionsMapper<>), typeof(QueryOptionsMapper<>));


services.AddApplication();

services.AddControllers(options =>
	{
		options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
	})
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
		options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
	});

services.AddAuthorization();
services.AddSwaggerGen(c => { c.OperationFilter<RemoveFiltersParameterOperationFilter>(); });

services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();
app.MapControllers();

app.UseHttpsRedirection();

app.Run();