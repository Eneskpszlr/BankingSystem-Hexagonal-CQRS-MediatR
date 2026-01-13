using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Exceptions;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Accounts
{
    public class GetAccountByIdUseCase : IGetAccountByIdUseCase
    {
        private readonly IAccountRepository _repository;
        public GetAccountByIdUseCase(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<Account> ExecuteAsync(int id, int userId)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null)
                throw new DomainException("Hesap bulunamadı.");

            if (account.CustomerId != userId)
            {
                throw new DomainException("Hesap bulunamadı.");
            }
            return account;
        }
    }
}
