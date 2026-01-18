namespace BankingHexagonal.Application.PrimaryPorts.CustomerPorts
{
    public interface IRemoveCustomerUseCase
    {
        Task ExecuteAsync(int id);
    }
}
