using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Exceptions;
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
        public async Task<Transaction> ExecuteAsync(int id, int userId)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null) 
                throw new DomainException("İşlem bulunamadı");

            if (transaction.Account.CustomerId != userId)
            {
                throw new DomainException("İşlem bulunamadı");
            }
            return transaction;
        }
    }
}
