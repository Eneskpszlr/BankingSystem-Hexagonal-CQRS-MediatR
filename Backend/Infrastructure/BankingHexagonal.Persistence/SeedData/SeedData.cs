using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Persistence.EFData;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Persistence
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<MyContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            // 1. Roller Var mı? Yoksa Ekle.
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole<int>("Admin"));

            if (!await roleManager.RoleExistsAsync("Customer"))
                await roleManager.CreateAsync(new IdentityRole<int>("Customer"));

            // 2. Admin Kullanıcısı Var mı? Yoksa Ekle.
            var adminUser = await userManager.FindByNameAsync("admin123");
            if (adminUser == null)
            {
                var admin = new AppUser
                {
                    UserName = "admin123", // Giriş için TCKN yerine bunu kullanacak
                    Email = "admin@hexabank.com",
                    CustomerNumber = "ADMIN001",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin123!"); // Şifre: Admin123!
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
