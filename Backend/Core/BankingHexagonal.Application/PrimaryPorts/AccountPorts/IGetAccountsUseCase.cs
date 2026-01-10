using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Application.PrimaryPorts.AccountPorts
{
    public interface IGetAccountsUseCase
    {
        Task<List<Account>> ExecuteAsync();
    }
}
