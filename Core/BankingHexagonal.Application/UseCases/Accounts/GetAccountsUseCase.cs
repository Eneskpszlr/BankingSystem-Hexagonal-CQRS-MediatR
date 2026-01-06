using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Accounts
{
    public class GetAccountsUseCase : IGetAccountsUseCase
    {
        private readonly IAccountRepository _repository;
        public GetAccountsUseCase(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Account>> ExecuteAsync()
        {
            return await _repository.GetAllAsync(tracking: false);
        }
    }
}
