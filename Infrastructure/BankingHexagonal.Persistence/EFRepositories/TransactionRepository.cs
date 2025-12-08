using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFData;

namespace BankingHexagonal.Persistence.EFRepositories
{
    public class TransactionRepository(MyContext context) : BaseRepository<Transaction>(context), ITransactionRepository
    {
    }
}
