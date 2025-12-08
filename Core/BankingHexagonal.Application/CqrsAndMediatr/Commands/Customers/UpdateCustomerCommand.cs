using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Customers;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers
{
    public class UpdateCustomerCommand : IRequest<UpdateCustomerCommandResult>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
