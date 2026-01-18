using BankingHexagonal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Persistence.EFConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AccountNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Status)
               .IsRequired()
               .HasConversion<int>();

            // --- VALUE OBJECT AYARI (MONEY) ---
            // Veritabanında ayrı tablo açmaz, Accounts tablosuna kolon olarak ekler.
            builder.OwnsOne(x => x.Balance, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("Balance") // Kolon adı: Balance
                    .HasPrecision(18, 4)      // 18 basamak, 4 ondalık
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("CurrencyCode") // Kolon adı: CurrencyCode
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // --- CONCURRENCY AYARI ---
            builder.Property(x => x.RowVersion)
                .IsRowVersion(); // Timestamp column

            // --- İLİŞKİLER ---
            builder.HasOne(x => x.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(x => x.BranchId);

            builder.HasOne(x => x.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // Müşteri silinirse hesap silinmesin
        }
    }
}
