using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;

namespace BankingHexagonal.Application.PrimaryPorts.AccountPorts
{
    public interface IUpdateAccountUseCase
    {
        Task ExecuteAsync(UpdateAccountCommand command);
    }
}
