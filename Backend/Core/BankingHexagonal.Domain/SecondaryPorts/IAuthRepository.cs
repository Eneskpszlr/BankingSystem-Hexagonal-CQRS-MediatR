using BankingHexagonal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Domain.SecondaryPorts
{
    public interface IAuthRepository
    {
        Task<(bool IsSuccess, string ErrorMessage)> RegisterUserAsync(AppUser user, string password, string role);
        Task<AppUser?> FindUserByTcknOrCustomerNumberAsync(string identifier);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
    }
}
