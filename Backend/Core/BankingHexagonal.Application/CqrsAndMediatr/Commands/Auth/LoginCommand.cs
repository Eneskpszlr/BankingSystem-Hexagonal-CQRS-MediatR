using BankingHexagonal.Application.DTOs.Auths;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Auth
{
    public class LoginCommand : IRequest<AuthResponse>
    {
        public string Identifier { get; set; } // TCKN veya Müşteri No
        public string Password { get; set; }
    }
}
