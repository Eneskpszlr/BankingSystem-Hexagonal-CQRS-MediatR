using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Transactions
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, GetTransactionByIdQueryResult>
    {
        private readonly IGetTransactionByIdUseCase _useCase;

        public GetTransactionByIdQueryHandler(IGetTransactionByIdUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<GetTransactionByIdQueryResult> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var t =  await _useCase.ExecuteAsync(request.Id);
            return new GetTransactionByIdQueryResult
            {
                Id = t.Id,
                Amount = t.Amount,
                Description = t.Description,
                TransactionType = t.TransactionType,
                TargetAccountId = t.TargetAccountId,
                AccountId = t.AccountId
            };
        }
    }
}
