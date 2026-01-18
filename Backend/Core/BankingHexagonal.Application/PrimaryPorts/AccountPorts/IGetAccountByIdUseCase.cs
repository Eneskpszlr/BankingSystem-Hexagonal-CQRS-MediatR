using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Application.PrimaryPorts.AccountPorts
{
    public interface IGetAccountByIdUseCase
    {
        Task<Account> ExecuteAsync(int id, int userId);
    }
}
