using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Branches
{
    public class GetBranchesUseCase : IGetBranchesUseCase
    {
        private readonly IBranchRepository _repository;
        public GetBranchesUseCase(IBranchRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Branch>> ExecuteAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
