using BankingHexagonal.Application.CqrsAndMediatr.Commands.Auth;
using BankingHexagonal.Application.DTOs.Auths;
using BankingHexagonal.Application.PrimaryPorts.AuthPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly ILoginUseCase _useCase;

        public LoginCommandHandler(ILoginUseCase useCase)
        {
            _useCase = useCase;
        }

        public Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return _useCase.ExecuteAsync(request.Identifier, request.Password);
        }
    }
}
