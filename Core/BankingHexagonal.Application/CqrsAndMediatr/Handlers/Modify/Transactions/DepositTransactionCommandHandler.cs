using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Transactions
{
    public class DepositTransactionCommandHandler : IRequestHandler<DepositTransactionCommand, DepositTransactionCommandResult>
    {
        private readonly IDepositUseCase _useCase;

        public DepositTransactionCommandHandler(IDepositUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<DepositTransactionCommandResult> Handle(DepositTransactionCommand request, CancellationToken cancellationToken)
        {
            // UseCase geriye TransactionId (int) dönmeli
            int transactionId = await _useCase.ExecuteAsync(request);

            return new DepositTransactionCommandResult
            {
                Success = true,
                Message = "Para yatırma işlemi başarılı.",
                EntityId = transactionId // İşlem ID'si
            };
        }
    }
}
