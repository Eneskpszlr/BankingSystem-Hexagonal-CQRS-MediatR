using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.PrimaryPorts.BranchPorts
{
    public interface ICreateBranchUseCase
    {
        Task<int> ExecuteAsync(CreateBranchCommand command);
    }
}
