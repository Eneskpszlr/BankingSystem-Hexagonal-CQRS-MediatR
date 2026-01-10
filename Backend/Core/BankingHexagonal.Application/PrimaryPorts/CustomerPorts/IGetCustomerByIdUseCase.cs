using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Application.PrimaryPorts.CustomerPorts
{
    public interface IGetCustomerByIdUseCase
    {
        Task<Customer> ExecuteAsync(int id);
    }
}
