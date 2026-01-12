using BankingHexagonal.Application.CqrsAndMediatr.Commands.Auth;
using BankingHexagonal.Application.PrimaryPorts.AuthPorts;
using BankingHexagonal.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Auth
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IRegisterUserUseCase _useCase;

        public RegisterUserCommandHandler(IRegisterUserUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            return await _useCase.ExecuteAsync(
                request.FirstName,
                request.LastName,
                request.Tckn,
                request.BirthDate,
                request.Password
            );
        }
    }
}
