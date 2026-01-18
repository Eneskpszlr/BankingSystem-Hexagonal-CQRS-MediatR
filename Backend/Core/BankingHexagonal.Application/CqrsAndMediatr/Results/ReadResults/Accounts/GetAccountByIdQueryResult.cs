namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts
{
    public class GetAccountByIdQueryResult
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }

        // --- Money Value Object ---
        public decimal Balance { get; set; }
        public string CurrencyCode { get; set; }

        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
    }
}
