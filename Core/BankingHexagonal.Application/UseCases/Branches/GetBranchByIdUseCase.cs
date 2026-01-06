using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Branches
{
    public class GetBranchByIdUseCase : IGetBranchByIdUseCase
    {
        private readonly IBranchRepository _repository;
        public GetBranchByIdUseCase(IBranchRepository repository)
        {
            _repository = repository;
        }
        public async Task<Branch> ExecuteAsync(int id)
        {
            var branch = await _repository.GetByIdAsync(id);
            if (branch == null) throw new Exception("Şube bulunamadı");
            return branch;
        }
    }
}
