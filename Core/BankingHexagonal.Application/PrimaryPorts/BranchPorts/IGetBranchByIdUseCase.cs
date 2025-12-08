using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Application.PrimaryPorts.BranchPorts
{
    public interface IGetBranchByIdUseCase
    {
        Task<Branch> ExecuteAsync(int id);
    }
}
