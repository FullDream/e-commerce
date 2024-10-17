using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
	public void Configure(EntityTypeBuilder<Image> builder)
	{
		builder
			.HasIndex(image => image.Src)
			.IsUnique();
	}
}