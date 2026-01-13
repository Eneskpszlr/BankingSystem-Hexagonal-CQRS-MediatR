using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Transactions;
using MediatR;
using System.Text.Json.Serialization;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions
{
    public class WithdrawTransactionCommand : IRequest<WithdrawTransactionCommandResult>
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string? Description { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }
}
