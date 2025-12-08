using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Customers;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers
{
    public class RemoveCustomerCommand : IRequest<RemoveCustomerCommandResult>
    {
        public RemoveCustomerCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
