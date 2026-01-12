using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFData;

namespace BankingHexagonal.Persistence.EFRepositories
{
    public class CustomerRepository(MyContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
        public async Task<Customer> AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
    }
}
