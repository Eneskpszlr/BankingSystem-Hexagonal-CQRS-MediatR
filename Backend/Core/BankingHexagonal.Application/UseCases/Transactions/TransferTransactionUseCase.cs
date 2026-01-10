using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Domain.ValueObjects;

namespace BankingHexagonal.Application.UseCases.Transactions
{
    public class TransferTransactionUseCase : ITransferUseCase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransferTransactionUseCase(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ExecuteAsync(TransferTransactionCommand command)
        {
            var fromAccount = await _accountRepository.GetByIdAsync(command.FromAccountId);
            var toAccount = await _accountRepository.GetByIdAsync(command.ToAccountId);

            if (fromAccount == null || toAccount == null)
                throw new Exception("Gönderen veya Alıcı hesap bulunamadı.");

            var money = new Money(command.Amount, command.CurrencyCode);
            string refNo = "TR-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            // 1. ADIM: Gönderen Hesaptan Çıkış (TransferOut)
            // Bu metot bakiyeyi düşer, Currency kontrolü yapar ve Out logu atar.
            fromAccount.TransferMoneyTo(toAccount, money, command.Description + " Ref:" + refNo);

            // 2. ADIM: Alıcı Hesaba Giriş (TransferIn)
            // Bu metot bakiyeyi artırır ve In logu atar.
            toAccount.ReceiveMoneyFrom(fromAccount.Id, money, command.Description);

            // 3. ADIM: Her ikisini tek transaction'da kaydet
            await _unitOfWork.SaveChangesAsync();

            return refNo;
        }
    }
}
