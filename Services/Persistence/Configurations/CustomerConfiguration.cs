using Customers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsOne(
            customer => customer.PhoneNumber,
            phoneNumber =>
            {
                phoneNumber.Property(prop => prop.CountryCode).HasColumnName("PhoneCountryCode");
                phoneNumber.Property(prop => prop.Number).HasColumnName("PhoneNumber");
            }
        );

        builder.OwnsOne(
            customer => customer.Telegram,
            telegram =>
            {
                telegram.Property(prop => prop.Id).HasColumnName("TelegramId");
            }
        );

        builder.OwnsMany(
            customer => customer.Addresses,
            address =>
            {
                address.ToTable("Addresses");
                address.WithOwner().HasForeignKey("CustomerId");
                address.HasKey("Id");
                address.OwnsOne(
                    address => address.Location,
                    location =>
                    {
                        location
                            .Property(prop => prop.Latitude)
                            .HasColumnName("Latitude")
                            .IsRequired();
                        location
                            .Property(prop => prop.Longitude)
                            .HasColumnName("Longitude")
                            .IsRequired();
                    }
                );
            }
        );
    }
}
