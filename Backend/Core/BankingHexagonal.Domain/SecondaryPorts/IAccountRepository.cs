using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Domain.SecondaryPorts
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account?> GetByIdWithDetailsAsync(int id);
        Task<Account?> GetByAccountNumberAsync(string accountNumber);
    }
}
