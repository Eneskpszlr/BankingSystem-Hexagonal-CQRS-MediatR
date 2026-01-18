using BankingHexagonal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankingHexagonal.Persistence.EFConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ReferenceNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(200);

            builder.Property(x => x.TransactionType)
                .HasConversion<string>()
                .IsRequired();

            // --- VALUE OBJECT AYARI (MONEY) ---
            builder.OwnsOne(x => x.Amount, money =>
            {
                money.Property(m => m.Amount)
                    .HasColumnName("Amount")
                    .HasPrecision(18, 4)
                    .IsRequired();

                money.Property(m => m.Currency)
                    .HasColumnName("CurrencyCode")
                    .HasMaxLength(3)
                    .IsRequired();
            });

            // --- KRİTİK İLİŞKİLER (Account <-> Transaction) ---

            // 1. İşlemi Yapan Hesap (Owner)
            builder.HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Restrict); // Hesap silinirse geçmiş silinmesin

            // 2. Transfer Yapılan Hesap (Target) - Nullable
            builder.HasOne(t => t.TargetAccount)
                .WithMany(a => a.IncomingTransactions)
                .HasForeignKey(t => t.TargetAccountId)
                .OnDelete(DeleteBehavior.Restrict); // Hedef hesap silinirse geçmiş silinmesin
        }
    }
}
