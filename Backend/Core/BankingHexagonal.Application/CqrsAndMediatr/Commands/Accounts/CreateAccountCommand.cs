using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Accounts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts
{
    public class CreateAccountCommand : IRequest<CreateAccountCommandResult>
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        public string CurrencyCode { get; set; }
    }
}
