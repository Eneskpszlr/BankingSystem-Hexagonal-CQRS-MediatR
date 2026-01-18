using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.Exceptions;
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
        public async Task<Customer> ExecuteAsync(int requestedId, int currentUserId, bool isAdmin)
        {

            if (!isAdmin && requestedId != currentUserId)
            {
                throw new DomainException("Sadece kendi profilinizi görüntüleyebilirsiniz.");
            }

            var customer = await _repository.GetByIdAsync(requestedId);
            if (customer == null) 
                throw new DomainException("Customer bulunamadı");
            return customer;
        }
    }
}
