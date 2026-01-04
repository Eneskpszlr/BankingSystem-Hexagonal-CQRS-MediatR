using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Domain.ValueObjects;

namespace BankingHexagonal.Application.UseCases.Transactions
{
    public class WithdrawTransactionUseCase : IWithdrawUseCase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public WithdrawTransactionUseCase(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(WithdrawTransactionCommand command)
        {
            var account = await _accountRepository.GetByIdAsync(command.AccountId);
            if (account == null) throw new Exception("Hesap bulunamadı.");

            var money = new Money(command.Amount, command.CurrencyCode);

            // Domain Metodu: Bakiye yetersizse Exception fırlatır, yeterliyse düşer ve log atar.
            account.Withdraw(money, command.Description);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
