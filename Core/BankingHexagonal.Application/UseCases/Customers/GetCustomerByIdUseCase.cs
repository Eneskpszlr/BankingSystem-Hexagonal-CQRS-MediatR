using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Customers
{
    public class GetCustomerByIdUseCase : IGetCustomerByIdUseCase
    {
        private readonly ICustomerRepository _repository;
        public GetCustomerByIdUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<Customer> ExecuteAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer bulunamadı");
            return customer;
        }
    }
}
