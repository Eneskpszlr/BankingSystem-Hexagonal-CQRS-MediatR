using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.PrimaryPorts.CustomerPorts
{
    public interface ICreateCustomerUseCase
    {
        Task<int> ExecuteAsync(CreateCustomerCommand command);
    }
}
