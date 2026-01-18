using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers
{
    public class CreateCustomerCommand : IRequest<CreateCustomerCommandResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // --- Adres Detayları (Value Object için) ---
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
