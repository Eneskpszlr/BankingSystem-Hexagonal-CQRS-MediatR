using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Transactions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions
{
    public class DepositTransactionCommand : IRequest<DepositTransactionCommandResult>
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string? Description { get; set; }
    }
}
