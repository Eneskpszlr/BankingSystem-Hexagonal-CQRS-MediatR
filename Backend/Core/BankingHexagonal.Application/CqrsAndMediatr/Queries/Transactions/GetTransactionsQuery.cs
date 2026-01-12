using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions
{
    public class GetTransactionsQuery : IRequest<List<GetTransactionsQueryResult>>
    {
        public int? AccountId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
