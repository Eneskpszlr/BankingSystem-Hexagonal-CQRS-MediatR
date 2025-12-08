using BankingHexagonal.Domain.Enums;

namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions
{
    public class GetTransactionByIdQueryResult
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }

        public int? TargetAccountId { get; set; }
        public int AccountId { get; set; }
    }
}
