using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Application.PrimaryPorts.TransactionPorts
{
    public interface IGetTransactionByIdUseCase
    {
        Task<Transaction> ExecuteAsync(int transactionId);
    }
}
