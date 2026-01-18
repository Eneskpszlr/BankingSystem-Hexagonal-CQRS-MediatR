using BankingHexagonal.Domain.Enums;

namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions
{
    public class GetTransactionByIdQueryResult
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? TargetAccountId { get; set; } // Transfer ise

        public string TransactionType { get; set; }
        public string Description { get; set; }
        public string ReferenceNumber { get; set; } //Dekont No

        // --- Money Value Object ---
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }

        public DateTime CreatedDate { get; set; } // İşlem tarihi
    }
}
