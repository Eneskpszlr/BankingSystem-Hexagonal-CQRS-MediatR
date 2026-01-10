using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Transactions
{
    public class TransferTransactionCommandHandler : IRequestHandler<TransferTransactionCommand, TransferTransactionCommandResult>
    {
        private readonly ITransferUseCase _useCase;
        public TransferTransactionCommandHandler(ITransferUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<TransferTransactionCommandResult> Handle(TransferTransactionCommand request, CancellationToken cancellationToken)
        {
            // UseCase geriye Dekont No (string) dönmeli
            string refNo = await _useCase.ExecuteAsync(request);

            return new TransferTransactionCommandResult
            {
                Success = true,
                Message = "Transfer işlemi başarılı.",
                ReferenceNumber = refNo
            };
        }
    }
}
