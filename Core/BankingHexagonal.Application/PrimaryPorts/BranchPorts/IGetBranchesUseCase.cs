using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Application.PrimaryPorts.BranchPorts
{
    public interface IGetBranchesUseCase
    {
        Task<List<Branch>> ExecuteAsync();
    }
}
