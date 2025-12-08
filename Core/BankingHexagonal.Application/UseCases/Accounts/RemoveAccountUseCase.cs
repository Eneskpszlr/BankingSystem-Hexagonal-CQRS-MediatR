using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Accounts
{
    public class RemoveAccountUseCase : IRemoveAccountUseCase
    {
        private readonly IAccountRepository _repository;
        public RemoveAccountUseCase(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(int id)
        {
            var exist = await _repository.GetByIdAsync(id);
            if (exist == null)
                throw new Exception("Hesap bulunamadı.");
            exist.Status = Domain.Enums.DataStatus.Deleted;
            exist.DeletedDate = DateTime.Now;
            await _repository.DeleteAsync(exist);
        }
    }
}
