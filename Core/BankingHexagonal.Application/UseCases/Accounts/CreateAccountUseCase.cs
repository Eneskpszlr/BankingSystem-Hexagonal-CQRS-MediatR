using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.UseCases.Accounts
{
    public class CreateAccountUseCase : ICreateAccountUseCase
    {
        private readonly IAccountRepository _repository;

        public CreateAccountUseCase(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(CreateAccountCommand command)
        {
            Account account = new Account
            {
                AccountNumber = command.AccountNumber,
                Balance = command.Balance,
                BranchId = command.BranchId,
                CustomerId = command.CustomerId,
                CreatedDate = DateTime.Now,
                Status = Domain.Enums.DataStatus.Inserted
            };

            await _repository.CreateAsync(account);
        }
    }
}
