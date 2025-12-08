using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Accounts
{
    public class UpdateAccountUseCase : IUpdateAccountUseCase
    {
        private readonly IAccountRepository _repository;
        public UpdateAccountUseCase(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(UpdateAccountCommand command)
        {
            var exist = await _repository.GetByIdAsync(command.Id);
            if (exist == null)
                throw new Exception("Hesap bulunamadı.");
            exist.AccountNumber = command.AccountNumber;
            exist.Balance = command.Balance;
            exist.BranchId = command.BranchId;
            exist.CustomerId = command.CustomerId;
            exist.Status = Domain.Enums.DataStatus.Updated;
            exist.UpdatedDate = DateTime.Now;
            await _repository.UpdateAsync(exist);
        }
    }
}
