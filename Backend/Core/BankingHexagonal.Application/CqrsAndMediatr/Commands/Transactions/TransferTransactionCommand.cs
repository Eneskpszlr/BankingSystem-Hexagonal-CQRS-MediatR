using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Transactions;
using MediatR;
using System.Text.Json.Serialization;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions
{
    public class TransferTransactionCommand : IRequest<TransferTransactionCommandResult>
    {
        public int FromAccountId { get; set; }
        public string ToAccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }
}
