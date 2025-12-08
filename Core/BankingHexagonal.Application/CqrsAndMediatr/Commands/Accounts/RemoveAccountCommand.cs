using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Accounts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts
{
    public class RemoveAccountCommand : IRequest<RemoveAccountCommandResult>
    {
        public RemoveAccountCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
