using BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Accounts
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, GetAccountByIdQueryResult>
    {
        private readonly IGetAccountByIdUseCase _useCase;
        public GetAccountByIdQueryHandler(IGetAccountByIdUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<GetAccountByIdQueryResult> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _useCase.ExecuteAsync(request.Id, request.UserId);

            return new GetAccountByIdQueryResult
            {
                Id = account.Id,
                AccountNumber = account.AccountNumber,

                // --- VALUE OBJECT MAPPING ---
                Balance = account.Balance.Amount,
                CurrencyCode = account.Balance.Currency,

                BranchId = account.BranchId,
                CustomerId = account.CustomerId
            };
        }
    }
}
