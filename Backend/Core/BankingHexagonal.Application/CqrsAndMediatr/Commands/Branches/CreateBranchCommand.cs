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

        // Adres Bilgileri (Value Object için gerekli parçalar)
        public string Street { get; set; }   // Cadde/Sokak/Mahalle
        public string City { get; set; }     // İl
        public string Country { get; set; }  // Ülke
        public string ZipCode { get; set; }  // Posta Kodu
    }
}
