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
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountUseCase(IAccountRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateAccountCommand command)
        {
            // 1. Rich Domain Model: Constructor üzerinden nesne oluşturulur.
            var account = new Account(
                command.AccountNumber,
                command.CustomerId,
                command.BranchId,
                command.CurrencyCode
            );

            // 2. Memory'e ekle
            await _repository.CreateAsync(account);

            // 3. Veritabanına kaydet (Transaction Commit)
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
