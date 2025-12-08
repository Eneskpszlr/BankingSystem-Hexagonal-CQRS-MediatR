namespace BankingHexagonal.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
