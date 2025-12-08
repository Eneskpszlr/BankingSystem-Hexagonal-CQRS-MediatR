using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Interfaces;

namespace BankingHexagonal.Domain.SecondaryPorts
{
    public interface IAccountRepository : IRepository<Account>
    {
    }
}
