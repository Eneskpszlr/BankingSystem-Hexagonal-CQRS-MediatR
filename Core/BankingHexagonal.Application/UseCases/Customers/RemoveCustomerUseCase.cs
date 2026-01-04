using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Customers
{
    public class RemoveCustomerUseCase : IRemoveCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCustomerUseCase(ICustomerRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) throw new Exception("Customer bulunamadı");

            _repository.Delete(customer);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
