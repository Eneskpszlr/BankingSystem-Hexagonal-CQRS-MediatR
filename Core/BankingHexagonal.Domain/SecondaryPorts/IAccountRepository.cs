using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Domain.SecondaryPorts
{
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
