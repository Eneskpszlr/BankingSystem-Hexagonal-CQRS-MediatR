using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Transactions
{
    public class WithdrawTransactionCommandHandler : IRequestHandler<WithdrawTransactionCommand, WithdrawTransactionCommandResult>
    {
        private readonly IWithdrawUseCase _useCase;
        public WithdrawTransactionCommandHandler(IWithdrawUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<WithdrawTransactionCommandResult> Handle(WithdrawTransactionCommand request, CancellationToken cancellationToken)
        {
            int transactionId = await _useCase.ExecuteAsync(request);

            return new WithdrawTransactionCommandResult
            {
                Success = true,
                Message = "Para çekme işlemi başarılı.",
                EntityId = transactionId
            };
        }
    }
}
