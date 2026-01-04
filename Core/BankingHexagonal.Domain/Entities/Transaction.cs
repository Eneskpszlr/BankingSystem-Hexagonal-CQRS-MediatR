using BankingHexagonal.Domain.Entities.Base;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.Exceptions;
using BankingHexagonal.Domain.ValueObjects;

namespace BankingHexagonal.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public int AccountId { get; private set; }
        public virtual Account Account { get; private set; }

        // Veritabanında Amount ve Currency kolonlarına map edilecek (Owned Type)
        public Money Amount { get; private set; }

        public TransactionType TransactionType { get; private set; }

        // Transfer ise paranın gittiği hesap
        public int? TargetAccountId { get; private set; }
        public virtual Account TargetAccount { get; private set; }

        public string Description { get; private set; }

        // Dekont Numarası (Benzersiz olmalı)
        public string ReferenceNumber { get; private set; }

        protected Transaction() { }

        // Constructor
        public Transaction(
            int accountId,
            decimal amount,
            TransactionType type,
            string description,
            string currencyCode,
            int? targetAccountId = null)
        {
            // 1. Value Object Oluşturma
            // Ancak Transaction'a özel "0 olamaz" kuralını burada da işletebiliriz.
            if (amount <= 0) throw new InvalidAmountException();

            Amount = new Money(amount, currencyCode);

            // 2. Transfer Kontrolü: Hedef Hesap Var mı?
            if ((type == TransactionType.TransferOut) && targetAccountId == null)
                throw new TargetAccountRequiredException();

            // 3. Kendine Transfer Kontrolü
            if (targetAccountId.HasValue && targetAccountId == accountId)
                throw new SameAccountTransferException();

            AccountId = accountId;
            TransactionType = type;
            Description = description;
            TargetAccountId = targetAccountId;

            // Benzersiz Referans No Üret (Örn: TR-8A2F9C)
            ReferenceNumber = "TR-" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();

            CreatedDate = DateTime.UtcNow;
            Status = DataStatus.Inserted;
        }
    }
}
