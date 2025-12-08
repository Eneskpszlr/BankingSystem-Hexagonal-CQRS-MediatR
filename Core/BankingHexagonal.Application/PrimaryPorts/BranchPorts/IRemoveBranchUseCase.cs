namespace BankingHexagonal.Application.PrimaryPorts.BranchPorts
{
    public interface IRemoveBranchUseCase
    {
        Task ExecuteAsync(int id);
    }
}
