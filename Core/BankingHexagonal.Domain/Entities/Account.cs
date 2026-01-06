using BankingHexagonal.Domain.Constants;
using BankingHexagonal.Domain.Entities.Base;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.Exceptions;
using BankingHexagonal.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace BankingHexagonal.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string AccountNumber { get; private set; }

        // CurrencyCode ve Amount bu nesnenin içinde tutuluyor.
        public Money Balance { get; private set; }

        // --- CONCURRENCY ---
        // Aynı anda iki işlem yapılırsa (Race Condition), EF Core bunu fark edip hata fırlatacak.
        [Timestamp]
        public byte[] RowVersion { get; set; }

        // --- NAVIGATION PROPERTIES ---
        public int BranchId { get; private set; }
        public virtual Branch Branch { get; private set; }

        public int CustomerId { get; private set; }
        public virtual Customer Customer { get; private set; }

        // Listeler dışarıdan 'Add' işlemine kapalı (Immutable Collection)
        private readonly List<Transaction> _transactions = new();
        public virtual IReadOnlyCollection<Transaction> Transactions => _transactions.AsReadOnly();

        // Bu hesaba dışarıdan gelen paralar (Transferler)
        private readonly List<Transaction> _incomingTransactions = new();
        public virtual IReadOnlyCollection<Transaction> IncomingTransactions => _incomingTransactions.AsReadOnly();

        // EF Core için boş constructor
        protected Account() { }

        // Hesap Oluşturma (Constructor)
        public Account(
            string accountNumber,
            int customerId,
            int branchId,
            string currencyCode = CurrencyCodes.TRY
        )
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new InvalidAccountNumberException();

            AccountNumber = accountNumber;
            CustomerId = customerId;
            BranchId = branchId;

            // Başlangıç bakiyesi 0 ve seçilen para birimi ile oluşturuluyor
            Balance = new Money(0, currencyCode);

            CreatedDate = DateTime.UtcNow;
            Status = DataStatus.Inserted;
        }


        // DOMAIN BEHAVIORS (İŞ KURALLARI)

        // 1. PARA YATIRMA
        public Transaction Deposit(Money amount, string description = "Para Yatırma")
        {
            ValidateAccountIsActive();

            // Money.Add metodu para birimi kontrolünü (CurrencyMismatch) otomatik yapar.
            Balance = Balance.Add(amount);

            var t = AddTransaction(TransactionType.Deposit, amount, description);

            UpdatedDate = DateTime.UtcNow;

            return t;
        }

        // 2. PARA ÇEKME
        public Transaction Withdraw(Money amount, string description = "Para Çekme")
        {
            ValidateAccountIsActive();

            // Bakiye Yetersiz kontrolü (State check)
            if (Balance.Amount < amount.Amount)
                throw new InsufficientBalanceException();

            // Money.Subtract metodu para birimi kontrolünü yapar.
            Balance = Balance.Subtract(amount);

            var t = AddTransaction(TransactionType.Withdraw, amount, description);

            UpdatedDate = DateTime.UtcNow;
            return t;
        }

        // 3. TRANSFER GÖNDERME (Giden Havale)
        public Transaction TransferMoneyTo(Account targetAccount, Money amount, string description = "Transfer Gönderimi")
        {
            ValidateAccountIsActive();

            // Transfer Özel Kontrolleri
            if (targetAccount == null)
                throw new TargetAccountRequiredException();

            if (this.Id == targetAccount.Id)
                throw new SameAccountTransferException();

            // Karşı tarafın para birimi kontrolü (Money.Currency ile erişiyoruz)
            if (this.Balance.Currency != targetAccount.Balance.Currency)
                throw new CurrencyMismatchException(this.Balance.Currency, targetAccount.Balance.Currency);

            if (Balance.Amount < amount.Amount)
                throw new InsufficientBalanceException();

            // İşlem
            // Money.Subtract para birimi uyuşmazlığını da kontrol eder.
            Balance = Balance.Subtract(amount);

            // Kayıt (TargetAccountId dolu gider)
            var t = AddTransaction(TransactionType.TransferOut, amount, description, targetAccount.Id);

            UpdatedDate = DateTime.UtcNow;
            return t;
        }

        // 4. TRANSFER ALMA (Gelen Havale)
        public Transaction ReceiveMoneyFrom(int senderAccountId, Money amount, string description = "Transfer Alımı")
        {
            ValidateAccountIsActive();

            // İşlem (Money.Add para birimi kontrolünü yapar)
            Balance = Balance.Add(amount);

            // Kayıt (TargetAccountId burada 'Parayı Gönderen' olarak tutulabilir veya null geçilebilir. 
            // Muhasebe mantığı için göndereni Target olarak işaretliyoruz.)
            var t = AddTransaction(TransactionType.TransferIn, amount, description, senderAccountId);

            UpdatedDate = DateTime.UtcNow;
            return t;
        }


        public void UpdateDetails(string newAccountNumber, int newBranchId)
        {
            ValidateAccountIsActive();

            if (string.IsNullOrWhiteSpace(newAccountNumber))
                throw new InvalidAccountNumberException();

            // İş Kuralları
            AccountNumber = newAccountNumber;
            BranchId = newBranchId;

            // Tarih ve Durum
            UpdatedDate = DateTime.UtcNow;
            Status = DataStatus.Updated;
        }


        // PRIVATE HELPERS

        private Transaction AddTransaction(
            TransactionType type,
            Money amount,
            string description,
            int? targetAccountId = null
        )
        {
            var transaction = new Transaction(
                accountId: Id,
                amount: amount.Amount,
                type: type,
                description: description,
                currencyCode: amount.Currency,
                targetAccountId: targetAccountId
            );

            _transactions.Add(transaction);

            return transaction;
        }

        private void ValidateAccountIsActive()
        {
            if (Status == DataStatus.Deleted)
                throw new AccountClosedException();
        }
    }
}
