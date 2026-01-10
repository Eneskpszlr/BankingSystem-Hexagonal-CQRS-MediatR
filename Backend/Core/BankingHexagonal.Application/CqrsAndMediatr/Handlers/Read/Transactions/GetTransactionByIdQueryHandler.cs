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
            var t = await _useCase.ExecuteAsync(request.Id);

            return new GetTransactionByIdQueryResult
            {
                Id = t.Id,
                AccountId = t.AccountId,
                TargetAccountId = t.TargetAccountId,
                TransactionType = t.TransactionType,
                Description = t.Description,

                Amount = t.Amount.Amount, //Tutar
                CurrencyCode = t.Amount.Currency, //Birim
                ReferenceNumber = t.ReferenceNumber, // Dekont No
                CreatedDate = t.CreatedDate  // İşlem Tarihi
            };
        }
    }
}
