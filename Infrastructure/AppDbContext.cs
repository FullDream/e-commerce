using Application;
using Core.Entities;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options), IApplicationDbContext
{
	public DbSet<Product> Products { get; init; }
	public DbSet<Category> Categories { get; init; }
	public DbSet<Image> Images { get; init; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new ProductConfiguration());
		modelBuilder.ApplyConfiguration(new CategoryConfiguration());
		modelBuilder.ApplyConfiguration(new ImageConfiguration());
	}
}