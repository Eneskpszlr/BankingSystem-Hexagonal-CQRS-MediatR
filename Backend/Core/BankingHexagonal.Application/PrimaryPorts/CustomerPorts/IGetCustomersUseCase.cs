using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Application.PrimaryPorts.CustomerPorts
{
    public interface IGetCustomersUseCase
    {
        Task<List<Customer>> ExecuteAsync();
    }
}
