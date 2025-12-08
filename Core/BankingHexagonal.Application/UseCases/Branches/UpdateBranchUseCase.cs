using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Branches
{
    public class UpdateBranchUseCase : IUpdateBranchUseCase
    {
        private readonly IBranchRepository _repository;
        public UpdateBranchUseCase(IBranchRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(UpdateBranchCommand command)
        {
            var exist = await _repository.GetByIdAsync(command.Id);
            if(exist == null)
                throw new Exception("Şube bulunamadı");
            exist.BranchName = command.BranchName;
            exist.Address = command.Address;
            exist.UpdatedDate = DateTime.Now;
            exist.Status = Domain.Enums.DataStatus.Updated;
            await _repository.UpdateAsync(exist);
        }
    }
}
