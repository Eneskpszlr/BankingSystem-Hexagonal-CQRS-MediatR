using BankingHexagonal.Application.DTOs.Auths;
using BankingHexagonal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.PrimaryPorts.AuthPorts
{
    public interface ITokenService
    {
        AuthResponse GenerateToken(AppUser user, string role);
    }
}
