using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Branches;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches
{
    public class RemoveBranchCommand : IRequest<RemoveBranchCommandResult>
    {
        public int Id { get; set; }
    }
}
