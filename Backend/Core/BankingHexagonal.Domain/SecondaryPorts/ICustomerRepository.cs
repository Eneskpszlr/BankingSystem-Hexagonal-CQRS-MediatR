using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Domain.SecondaryPorts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> AddAsync(Customer customer);
    }
}
