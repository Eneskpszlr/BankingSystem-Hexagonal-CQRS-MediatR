using BankingHexagonal.Application.DTOs.Auths;
using BankingHexagonal.Application.PrimaryPorts.AuthPorts;
using BankingHexagonal.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthResponse GenerateToken(AppUser user, string role)
        {
            // 1. Token İçine Gömülecek Bilgiler
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // User ID
                new Claim(ClaimTypes.Name, user.UserName), // TCKN
                new Claim(JwtRegisteredClaimNames.Sub, user.CustomerId.ToString()),
                new Claim(ClaimTypes.Role, role) // Admin mi Customer mı?
            };

            // 2. Anahtar ve İmza
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddMinutes(double.Parse(_configuration["JwtSettings:DurationInMinutes"]));

            // 3. Token Oluşturma
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiry,
                Role = role,
                CustomerName = user.Customer != null ? $"{user.Customer.FirstName} {user.Customer.LastName}" : user.UserName
            };
        }
    }
}
