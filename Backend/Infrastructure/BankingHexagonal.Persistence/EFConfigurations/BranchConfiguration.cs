using BankingHexagonal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingHexagonal.Persistence.EFConfigurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            // --- VALUE OBJECT (ADDRESS) ---
            // Veritabanında Address_City, Address_Street gibi kolonlar oluşur
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
