using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Accounts
{
    public class UpdateAccountUseCase : IUpdateAccountUseCase
    {
        private readonly IAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAccountUseCase(IAccountRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task ExecuteAsync(UpdateAccountCommand command)
        {
            var account = await _repository.GetByIdAsync(command.Id);
            if (account == null) throw new Exception("Hesap bulunamadı.");

            account.UpdateDetails(
            command.AccountNumber,
            command.BranchId,
            (BankingHexagonal.Domain.Enums.DataStatus)command.Status
        );

            // Repoda Update çağırmaya gerek yok (Tracking açık).
            // _repository.Update(account);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
