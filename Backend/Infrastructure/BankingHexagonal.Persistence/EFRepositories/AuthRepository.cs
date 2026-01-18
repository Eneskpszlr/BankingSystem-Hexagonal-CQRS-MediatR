using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Persistence.EFRepositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUserAsync(AppUser user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var error = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, error);
            }

            await _userManager.AddToRoleAsync(user, role);
            return (true, string.Empty);
        }
        public async Task<AppUser?> FindUserByTcknOrCustomerNumberAsync(string identifier)
        {
            // Giriş yapılan değer TCKN mi Müşteri No mu?
            // Önce UserName (TCKN) olarak ara
            var user = await _userManager.Users
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.UserName == identifier);

            if (user != null) return user;

            // Bulamazsa CustomerNumber olarak ara
            return await _userManager.Users
                .Include(u => u.Customer)
                .FirstOrDefaultAsync(u => u.CustomerNumber == identifier);
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IList<string>> GetUserRolesAsync(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
}
