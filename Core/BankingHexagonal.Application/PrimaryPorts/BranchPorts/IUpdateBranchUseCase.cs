using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;

namespace BankingHexagonal.Application.PrimaryPorts.BranchPorts
{
    public interface IUpdateBranchUseCase
    {
        Task ExecuteAsync(UpdateBranchCommand command);
    }
}
