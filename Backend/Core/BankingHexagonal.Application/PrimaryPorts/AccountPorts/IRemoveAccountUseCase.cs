namespace BankingHexagonal.Application.PrimaryPorts.AccountPorts
{
    public interface IRemoveAccountUseCase
    {
        Task ExecuteAsync(int id);
    }
}
