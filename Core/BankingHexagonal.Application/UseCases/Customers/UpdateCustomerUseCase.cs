using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Domain.ValueObjects;

namespace BankingHexagonal.Application.UseCases.Customers
{
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerUseCase(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(UpdateCustomerCommand command)
        {
            var customer = await _repository.GetByIdAsync(command.Id);
            if (customer == null) throw new Exception("Customer bulunamadı");

            // 1. İsim Güncelleme
            customer.UpdateName(command.FirstName, command.LastName);

            // 2. İletişim Bilgileri Güncelleme
            customer.UpdateContactInfo(command.Phone, command.Email);

            // 3. Adres Güncelleme
            var newAddress = new Address(
                command.Street,
                command.City,
                command.Country,
                command.ZipCode
            );
            customer.UpdateAddress(newAddress);

            // 4. Kayıt
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
