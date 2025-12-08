using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.SecondaryPorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.UseCases.Transactions
{
    public class DepositTransactionUseCase : IDepositUseCase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public DepositTransactionUseCase(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        public async Task ExecuteAsync(DepositTransactionCommand command)
        {
            var account = await _accountRepository.GetByIdAsync(command.AccountId);
            if (account == null)
                throw new Exception("Hesap bulunamadı.");

            account.Balance += command.Amount;
            account.UpdatedDate = DateTime.Now;

            await _accountRepository.UpdateAsync(account);

            Transaction t = new Transaction
            {
                AccountId = account.Id,
                Amount = command.Amount,
                TransactionType = TransactionType.Deposit,
                Description = command.Description,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _transactionRepository.CreateAsync(t);
        }
    }
}
