using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Customers;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers
{
    public class UpdateCustomerCommand : IRequest<UpdateCustomerCommandResult>
    {
        public int Id { get; set; }

        // İsim değişikliği
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // İletişim Bilgileri
        public string Email { get; set; }
        public string Phone { get; set; }

        // --- Yeni Adres Bilgileri ---
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
