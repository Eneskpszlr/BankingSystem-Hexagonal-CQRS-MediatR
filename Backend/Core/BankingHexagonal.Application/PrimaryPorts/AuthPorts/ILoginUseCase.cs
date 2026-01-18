using BankingHexagonal.Application.DTOs.Auths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.PrimaryPorts.AuthPorts
{
    public interface ILoginUseCase
    {
        Task<AuthResponse> ExecuteAsync(string identifier, string password);
    }
}
