namespace Infrastructure.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// public class ProductAndProductTranslationConfiguration : IEntityTypeConfiguration<Product>, IEntityTypeConfiguration<ProductTranslation>
// {
// 	public void Configure(EntityTypeBuilder<Product> builder)
// 	{
// 		builder.HasMany(p => p.Translations)
// 			.WithOne(t => t.Product)
// 			.HasForeignKey(t => t.ProductId);
// 	}
//
// 	public void Configure(EntityTypeBuilder<ProductTranslation> builder)
// 	{
// 		builder.HasIndex(t => new { t.ProductId, t.Language })
// 			.IsUnique();
// 	}
// }
