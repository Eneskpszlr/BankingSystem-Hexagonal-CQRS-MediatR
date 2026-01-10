using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Transactions
{
    public class GetTransactionByIdUseCase : IGetTransactionByIdUseCase
    {
        private readonly ITransactionRepository _repository;
        public GetTransactionByIdUseCase(ITransactionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Transaction> ExecuteAsync(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null) throw new Exception("İşlem bulunamadı");
            return transaction;
        }
    }
}
