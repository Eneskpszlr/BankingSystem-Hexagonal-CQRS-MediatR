using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Domain.ValueObjects;

namespace BankingHexagonal.Application.UseCases.Customers
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork; // EKLENDİ

        public CreateCustomerUseCase(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(CreateCustomerCommand command)
        {
            // 1. Value Object: Adresi oluştur
            var address = new Address(
                command.Street,
                command.City,
                command.Country,
                command.ZipCode
            );

            // 2. Entity Oluşturma
            var customer = new Customer(
                command.FirstName,
                command.LastName,
                command.IdentityNumber,
                command.Email,
                address,
                command.Phone
            );

            // Kayıt
            await _repository.CreateAsync(customer);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
