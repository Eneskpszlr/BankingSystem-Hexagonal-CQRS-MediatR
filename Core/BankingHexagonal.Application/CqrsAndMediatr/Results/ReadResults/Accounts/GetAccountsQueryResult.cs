using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts
{
    public class GetAccountsQueryResult
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyCode { get; set; }
        public int BranchId { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }
    }
}
