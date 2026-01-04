using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Domain.ValueObjects;
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
        private readonly IUnitOfWork _unitOfWork;

        public DepositTransactionUseCase(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(DepositTransactionCommand command)
        {
            var account = await _accountRepository.GetByIdAsync(command.AccountId);
            if (account == null) throw new Exception("Hesap bulunamadı.");

            // 1. Value Object Oluştur
            var money = new Money(command.Amount, command.CurrencyCode);

            // 2. Domain Metodunu Çağır
            // Bu metot: Bakiyeyi artırır + Transaction kaydını oluşturup listeye ekler.
            account.Deposit(money, command.Description);

            // 3. Kaydet (EF Core, Account'u ve içindeki yeni Transaction'ı tek seferde yazar)
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
