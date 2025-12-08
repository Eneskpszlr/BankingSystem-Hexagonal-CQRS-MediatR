using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Customers
{
    public class RemoveCustomerUseCase : IRemoveCustomerUseCase
    {
        private readonly ICustomerRepository _repository;
        public RemoveCustomerUseCase(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(int id)
        {
            var exist = await _repository.GetByIdAsync(id);
            if (exist == null)
                throw new Exception("Customer bulunamadı");
            exist.Status = Domain.Enums.DataStatus.Deleted;
            exist.DeletedDate = DateTime.Now;
            await _repository.DeleteAsync(exist);
        }
    }
}
