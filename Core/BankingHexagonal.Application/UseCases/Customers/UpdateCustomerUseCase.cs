using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Customers
{
    public class UpdateCustomerUseCase : IUpdateCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        public UpdateCustomerUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(UpdateCustomerCommand command)
        {
            var exist = await _repository.GetByIdAsync(command.Id);
            if (exist == null)
                throw new Exception("Customer bulunamadı");
            exist.FirstName = command.FirstName;
            exist.LastName = command.LastName;
            exist.IdentityNumber = command.IdentityNumber;
            exist.Address = command.Address;
            exist.Phone = command.Phone;
            exist.Email = command.Email;
            exist.UpdatedDate = DateTime.Now;
            exist.Status = Domain.Enums.DataStatus.Updated;
            await _repository.UpdateAsync(exist);
        }
    }
}
