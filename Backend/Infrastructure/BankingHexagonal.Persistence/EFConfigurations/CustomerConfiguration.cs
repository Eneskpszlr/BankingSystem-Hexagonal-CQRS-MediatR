using BankingHexagonal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingHexagonal.Persistence.EFConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(11); // TCKN
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.IdentityNumber).IsUnique();

            // --- VALUE OBJECT (ADDRESS) ---
            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.Street).HasColumnName("Address_Street").HasMaxLength(150);
                address.Property(a => a.City).HasColumnName("Address_City").HasMaxLength(50);
                address.Property(a => a.Country).HasColumnName("Address_Country").HasMaxLength(50);
                address.Property(a => a.ZipCode).HasColumnName("Address_ZipCode").HasMaxLength(20);
            });
        }
    }
}
