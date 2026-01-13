using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Transactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<GetTransactionsQueryResult>>
    {
        private readonly IGetTransactionsUseCase _useCase;

        public GetTransactionsQueryHandler(IGetTransactionsUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<List<GetTransactionsQueryResult>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _useCase.ExecuteAsync(request);

            return transactions.Select(t => new GetTransactionsQueryResult
            {
                Id = t.Id,
                AccountId = t.AccountId,
                TargetAccountId = t.TargetAccountId,
                TransactionType = t.TransactionType,
                Description = t.Description,

                Amount = t.Amount.Amount,
                CurrencyCode = t.Amount.Currency,
                ReferenceNumber = t.ReferenceNumber,
                CreatedDate = t.CreatedDate
            }).ToList();
        }
    }
}
