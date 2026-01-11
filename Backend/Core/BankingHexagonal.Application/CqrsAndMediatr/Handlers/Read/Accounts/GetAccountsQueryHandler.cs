using BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
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
        private readonly IGetAccountsUseCase _useCase;

        public GetAccountsQueryHandler(IGetAccountsUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<List<GetAccountsQueryResult>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _useCase.ExecuteAsync();

            return accounts.Select(a => new GetAccountsQueryResult
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,

                // --- VALUE OBJECT MAPPING ---
                Balance = a.Balance.Amount,
                CurrencyCode = a.Balance.Currency,

                BranchId = a.BranchId,
                CustomerId = a.CustomerId,
                Status = a.Status.ToString()
            }).ToList();
        }
    }
}
