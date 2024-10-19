using Api.Filters;
using Api.Routing;
using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddDbInfrastructure(builder.Configuration);
services.AddMappingInfrastructure();
services.AddControllers(options =>
{
	options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer()));
	options.Filters.Add<ApiExceptionFilter>();
});

services.AddAuthorization();
services.AddSwaggerGen();


services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<AppDbContext>();

services.AddScoped<IProductService, ProductService>();
services.AddScoped<IProductRepository, ProductRepository>();

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