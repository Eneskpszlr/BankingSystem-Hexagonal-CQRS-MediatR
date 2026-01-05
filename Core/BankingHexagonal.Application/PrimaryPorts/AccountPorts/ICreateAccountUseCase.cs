using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.PrimaryPorts.AccountPorts
{
    public interface ICreateAccountUseCase
    {
        Task<int> ExecuteAsync(CreateAccountCommand command);
    }
}
