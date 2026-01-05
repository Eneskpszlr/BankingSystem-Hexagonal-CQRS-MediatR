using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;

namespace BankingHexagonal.Application.PrimaryPorts.TransactionPorts
{
    public interface IWithdrawUseCase
    {
        Task<int> ExecuteAsync(WithdrawTransactionCommand command);
    }
}
