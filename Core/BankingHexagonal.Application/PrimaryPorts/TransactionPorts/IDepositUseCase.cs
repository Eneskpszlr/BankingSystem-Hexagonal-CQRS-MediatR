using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;

namespace BankingHexagonal.Application.PrimaryPorts.TransactionPorts
{
    public interface IDepositUseCase
    {
        Task<int> ExecuteAsync(DepositTransactionCommand command);
    }
}
