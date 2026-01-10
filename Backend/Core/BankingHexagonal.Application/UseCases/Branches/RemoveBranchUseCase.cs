using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Branches
{
    public class RemoveBranchUseCase : IRemoveBranchUseCase
    {
        private readonly IBranchRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveBranchUseCase(IBranchRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task ExecuteAsync(int id)
        {
            var branch = await _repository.GetByIdAsync(id);
            if (branch == null) throw new Exception("Şube bulunamadı");
                
            _repository.Delete(branch);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
