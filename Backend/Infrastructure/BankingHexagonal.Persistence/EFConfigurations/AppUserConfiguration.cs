using BankingHexagonal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Persistence.EFConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasOne(u => u.Customer)
                   .WithOne()
                   .HasForeignKey<AppUser>(u => u.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(u => u.CustomerNumber).IsUnique();
        }
    }
}
