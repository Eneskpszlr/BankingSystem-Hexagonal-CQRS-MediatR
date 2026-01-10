using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Accounts
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountCommandResult>
    {
        private readonly ICreateAccountUseCase _useCase;

        public CreateAccountCommandHandler(ICreateAccountUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CreateAccountCommandResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            int createdAccountId = await _useCase.ExecuteAsync(request);

            return new CreateAccountCommandResult
            {
                Success = true,
                Message = "Hesap başarıyla oluşturuldu.",
                EntityId = createdAccountId
            };
        }
    }
}
