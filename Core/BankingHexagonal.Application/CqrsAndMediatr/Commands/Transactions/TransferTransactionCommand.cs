using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Transactions;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions
{
    public class TransferTransactionCommand : IRequest<TransferTransactionCommandResult>
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
