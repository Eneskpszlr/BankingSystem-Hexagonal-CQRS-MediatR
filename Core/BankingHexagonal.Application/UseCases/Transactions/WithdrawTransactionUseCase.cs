using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Transactions
{
    public class WithdrawTransactionUseCase : IWithdrawUseCase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WithdrawTransactionUseCase(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task ExecuteAsync(WithdrawTransactionCommand command)
        {
            var account = await _accountRepository.GetByIdAsync(command.AccountId);
            if (account == null)
                throw new Exception("Hesap bulunamadı.");

            if (account.Balance < command.Amount)
                throw new Exception("Yetersiz bakiye.");

            account.Balance -= command.Amount;
            account.UpdatedDate = DateTime.Now;

            await _accountRepository.UpdateAsync(account);

            Transaction t = new Transaction
            {
                AccountId = account.Id,
                Amount = -command.Amount,
                TransactionType = TransactionType.Withdraw,
                Description = command.Description,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            };

            await _transactionRepository.CreateAsync(t);
        }
    }
}
