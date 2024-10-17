using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder
			.HasMany(product => product.Images)
			.WithMany();

		builder
			.HasIndex(product => product.Slug)
			.IsUnique();
	}
}