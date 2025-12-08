using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Branches;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches
{
    public class UpdateBranchCommand : IRequest<UpdateBranchCommandResult>
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
    }
}
