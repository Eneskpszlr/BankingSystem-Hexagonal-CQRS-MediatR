using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;

namespace BankingHexagonal.Application.PrimaryPorts.TransactionPorts
{
    public interface ITransferUseCase
    {
        Task<string> ExecuteAsync(TransferTransactionCommand command);
    }
}
