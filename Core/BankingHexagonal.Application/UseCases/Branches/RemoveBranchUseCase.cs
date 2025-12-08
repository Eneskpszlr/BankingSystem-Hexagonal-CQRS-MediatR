using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Branches
{
    public class RemoveBranchUseCase : IRemoveBranchUseCase
    {
        private readonly IBranchRepository _repository;
        public RemoveBranchUseCase(IBranchRepository repository)
        {
            _repository = repository;
        }
        public async Task ExecuteAsync(int id)
        {
            var exist = await _repository.GetByIdAsync(id);
            if (exist == null)
                throw new Exception("Şube bulunamadı");
            exist.Status = Domain.Enums.DataStatus.Deleted;
            exist.DeletedDate = DateTime.Now;
            await _repository.DeleteAsync(exist);
        }
    }
}
