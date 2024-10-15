using Api.Routing;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddDbInfrastructure(builder.Configuration);
services.AddControllers(options =>
	options.Conventions.Add(new RouteTokenTransformerConvention(new KebabCaseParameterTransformer())));
services.AddAuthorization();
services.AddSwaggerGen();


services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<AppDbContext>();

services.AddScoped<IProductService, ProductService>();

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