using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFData;

namespace BankingHexagonal.Persistence.EFRepositories
{
    public class BranchRepository(MyContext context) : BaseRepository<Branch>(context), IBranchRepository
    {
    }
}
