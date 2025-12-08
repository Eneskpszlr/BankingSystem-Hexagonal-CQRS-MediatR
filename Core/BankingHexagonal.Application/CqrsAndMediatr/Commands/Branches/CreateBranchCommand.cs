using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Branches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches
{
    public class CreateBranchCommand : IRequest<CreateBranchCommandResult>
    {
        public string BranchName { get; set; }
        public string Address { get; set; }
    }
}
