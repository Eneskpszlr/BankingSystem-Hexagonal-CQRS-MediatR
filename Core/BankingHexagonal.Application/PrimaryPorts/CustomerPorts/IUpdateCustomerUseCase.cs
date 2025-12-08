using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;

namespace BankingHexagonal.Application.PrimaryPorts.CustomerPorts
{
    public interface IUpdateCustomerUseCase
    {
        Task ExecuteAsync(UpdateCustomerCommand command);
    }
}
