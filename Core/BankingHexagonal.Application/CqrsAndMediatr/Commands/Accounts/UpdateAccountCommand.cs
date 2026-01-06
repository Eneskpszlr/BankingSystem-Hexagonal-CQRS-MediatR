using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Accounts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts
{
    public class UpdateAccountCommand : IRequest<UpdateAccountCommandResult>
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public int BranchId { get; set; }
    }
}
