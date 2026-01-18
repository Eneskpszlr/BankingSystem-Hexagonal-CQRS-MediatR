using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFData;

namespace BankingHexagonal.Persistence.EFRepositories
{
    public class CustomerRepository(MyContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
    }
}
