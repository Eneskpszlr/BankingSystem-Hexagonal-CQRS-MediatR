using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Customers
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        public CreateCustomerUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(CreateCustomerCommand command)
        {
            Customer customer = new Customer
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                IdentityNumber = command.IdentityNumber,
                Address = command.Address,
                Phone = command.Phone,
                Email = command.Email,
                CreatedDate = DateTime.Now,
                Status = Domain.Enums.DataStatus.Inserted
            };
            await _repository.CreateAsync(customer);
        }
    }
}
