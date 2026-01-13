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
        public async Task<List<Account>> ExecuteAsync(int userId, bool isAdmin)
        {
            if (isAdmin)
            {
                // Admin hepsini görebilir
                return await _repository.GetAllAsync(tracking: false);
            }
            else
            {
                // Müşteri sadece kendi cüzdanını görür
                return await _repository.GetByCustomerIdAsync(userId);
            }
        }
    }
}
