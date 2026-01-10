using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts
{
    public class GetAccountsQuery : IRequest<List<GetAccountsQueryResult>>
    {
    }
}
