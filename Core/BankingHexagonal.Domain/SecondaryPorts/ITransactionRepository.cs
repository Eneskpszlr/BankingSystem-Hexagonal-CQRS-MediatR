using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Domain.SecondaryPorts
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
    }
}
