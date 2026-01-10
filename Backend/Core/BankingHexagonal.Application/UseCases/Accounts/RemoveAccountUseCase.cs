using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Accounts
{
    public class RemoveAccountUseCase : IRemoveAccountUseCase
    {
        private readonly IAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveAccountUseCase(IAccountRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null) throw new Exception("Hesap bulunamadı.");

            // Repository delete metodunu çağırıyoruz.
            _repository.Delete(account);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
