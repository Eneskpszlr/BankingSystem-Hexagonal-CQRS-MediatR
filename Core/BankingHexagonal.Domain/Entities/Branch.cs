namespace BankingHexagonal.Domain.Entities
{
    public class Branch : BaseEntity
    {
        public string BranchName { get; set; }
        public string Address { get; set; }

        // Navigation
        public ICollection<Account> Accounts { get; set; }
    }
}
