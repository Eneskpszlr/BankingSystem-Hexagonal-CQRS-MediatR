using BankingHexagonal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions
{
    public class GetTransactionsQueryResult
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }

        public int? TargetAccountId { get; set; }
        public int AccountId { get; set; }
    }
}
