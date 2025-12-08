using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Transactions
{
    public class TransferTransactionUseCase : ITransferUseCase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransferTransactionUseCase(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task ExecuteAsync(TransferTransactionCommand command)
        {
            var from = await _accountRepository.GetByIdAsync(command.FromAccountId);
            var to = await _accountRepository.GetByIdAsync(command.ToAccountId);

            if (from == null || to == null)
                throw new Exception("Hesap bulunamadı.");

            if (from.Balance < command.Amount)
                throw new Exception("Yetersiz bakiye.");

            from.Balance -= command.Amount;
            from.UpdatedDate = DateTime.Now;
            await _accountRepository.UpdateAsync(from);

            to.Balance += command.Amount;
            to.UpdatedDate = DateTime.Now;
            await _accountRepository.UpdateAsync(to);

            // Transaction log – gönderici
            await _transactionRepository.CreateAsync(new Transaction
            {
                AccountId = from.Id,
                Amount = -command.Amount,
                TransactionType = TransactionType.TransferOut,
                Description = command.Description,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            });

            // Transaction log – alıcı
            await _transactionRepository.CreateAsync(new Transaction
            {
                AccountId = to.Id,
                Amount = command.Amount,
                TransactionType = TransactionType.TransferIn,
                Description = command.Description,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted
            });
        }
    }
}
