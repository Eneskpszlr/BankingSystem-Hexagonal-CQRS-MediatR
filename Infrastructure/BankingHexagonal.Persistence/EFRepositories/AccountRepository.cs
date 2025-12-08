using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFData;

namespace BankingHexagonal.Persistence.EFRepositories
{
    public class AccountRepository(MyContext context) : BaseRepository<Account>(context), IAccountRepository
    {
    }
}
