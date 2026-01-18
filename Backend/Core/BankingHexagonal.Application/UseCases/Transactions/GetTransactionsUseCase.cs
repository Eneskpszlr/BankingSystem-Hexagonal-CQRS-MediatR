using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Exceptions;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Transactions
{
    public class GetTransactionsUseCase : IGetTransactionsUseCase
    {
        private readonly ITransactionRepository _repository;
        private readonly IAccountRepository _accountRepository;
        public GetTransactionsUseCase(ITransactionRepository repository, IAccountRepository accountRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
        }
        public async Task<List<Transaction>> ExecuteAsync(GetTransactionsQuery query)
        {
            if (query.AccountId.HasValue)
            {
                var account = await _accountRepository.GetByIdAsync(query.AccountId.Value);

                if (account == null || account.CustomerId != query.UserId)
                    throw new DomainException("Hesap bulunamadı veya size ait değil.");
            }

            return await _repository.GetFilteredAsync(
                query.UserId,
                query.AccountId,
                query.StartDate,
                query.EndDate
            );
        }
    }
}
