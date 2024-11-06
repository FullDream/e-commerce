using System.Text.Json;
using Api.Routing;
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

services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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
services.AddSwaggerGen();


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