using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Customers
{
    public class GetCustomersUseCase : IGetCustomersUseCase
    {
        private readonly ICustomerRepository _repository;
        public GetCustomersUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Customer>> ExecuteAsync()
        {
            return await _repository.GetAllAsync(tracking: false);
        }
    }
}
