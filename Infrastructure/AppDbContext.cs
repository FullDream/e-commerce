using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
{
	public DbSet<Product> Products { get; init; }
	public DbSet<Category> Categories { get; init; }
	public DbSet<Image> Images { get; init; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// modelBuilder.ApplyConfiguration(new ProductAndProductTranslationConfiguration());
		modelBuilder.Entity<Product>()
			.HasMany(p => p.Images)
			.WithMany();

		modelBuilder.Entity<Product>()
			.HasIndex(p => p.Slug)
			.IsUnique();
	}
}