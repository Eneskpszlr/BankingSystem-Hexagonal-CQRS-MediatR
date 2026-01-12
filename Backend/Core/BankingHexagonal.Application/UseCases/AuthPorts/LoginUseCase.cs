using BankingHexagonal.Application.DTOs.Auths;
using BankingHexagonal.Application.PrimaryPorts.AuthPorts;
using BankingHexagonal.Domain.Exceptions;
using BankingHexagonal.Domain.SecondaryPorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.UseCases.AuthPorts
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;

        public LoginUseCase(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> ExecuteAsync(string identifier, string password)
        {
            // 1. Kullanıcıyı Bul
            var user = await _authRepository.FindUserByTcknOrCustomerNumberAsync(identifier);

            if (user == null)
                throw new DomainException("Kullanıcı adı veya şifre hatalı.");

            // 2. Şifreyi Kontrol Et
            var isPasswordValid = await _authRepository.CheckPasswordAsync(user, password);
            if (!isPasswordValid)
                throw new DomainException("Kullanıcı adı veya şifre hatalı.");

            // 3. Rolü Al
            var roles = await _authRepository.GetUserRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Customer";

            // 4. Token Üret
            return _tokenService.GenerateToken(user, role);
        }
    }
}
