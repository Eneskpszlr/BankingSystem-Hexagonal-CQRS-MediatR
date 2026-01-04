using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Branches;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches
{
    public class UpdateBranchCommand : IRequest<UpdateBranchCommandResult>
    {
        public int Id { get; set; }
        public string BranchName { get; set; }

        // --- Yeni Adres Bilgileri ---
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
