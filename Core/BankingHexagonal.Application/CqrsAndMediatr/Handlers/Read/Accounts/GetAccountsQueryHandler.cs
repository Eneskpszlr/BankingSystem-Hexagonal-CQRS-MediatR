using BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using BankingHexagonal.Domain.SecondaryPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Accounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<GetAccountsQueryResult>>
    {
        private readonly IAccountRepository _repository;

        public GetAccountsQueryHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAccountsQueryResult>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            
        }
    }
}
