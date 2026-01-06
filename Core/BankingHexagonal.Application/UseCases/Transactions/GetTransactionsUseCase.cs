using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Transactions
{
    public class GetTransactionsUseCase : IGetTransactionsUseCase
    {
        private readonly ITransactionRepository _repository;
        public GetTransactionsUseCase(ITransactionRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Transaction>> ExecuteAsync()
        {
            return await _repository.GetAllAsync(tracking: false);
        }
    }
}
