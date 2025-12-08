using BankingHexagonal.Domain.Enums;

namespace BankingHexagonal.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; } // Deposit, Withdraw, Transfer
        public int? TargetAccountId { get; set; } // Transfer için
        public string Description { get; set; }

        public virtual Account Account { get; set; }
        public virtual Account TargetAccount { get; set; }
    }
}
