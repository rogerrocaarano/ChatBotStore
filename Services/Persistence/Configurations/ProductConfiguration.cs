using Catalogs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Description).HasMaxLength(500);
        builder.Property(p => p.IsAvailable).IsRequired();

        builder
            .HasOne<Catalog>()
            .WithMany()
            .HasForeignKey(p => p.CatalogId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(
            p => p.Price,
            price =>
            {
                price.Property(prop => prop.Amount).HasColumnName("PriceAmount");
                price.Property(prop => prop.Currency).HasColumnName("PriceCurrency");
            }
        );
    }
}
