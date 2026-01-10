using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.Validators.Branches
{
    public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
    {
        public CreateBranchValidator()
        {
            // 1. Şube Adı
            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("Şube adı boş olamaz.")
                .MaximumLength(100).WithMessage("Şube adı 100 karakterden uzun olamaz.");

            // 2. Adres Detayları (Value Object Kuralları)

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Cadde/Sokak bilgisi boş olamaz.")
                .MaximumLength(150).WithMessage("Cadde/Sokak bilgisi çok uzun.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir bilgisi boş olamaz.")
                .MaximumLength(50).WithMessage("Şehir adı çok uzun.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Ülke bilgisi boş olamaz.")
                .MaximumLength(50).WithMessage("Ülke adı çok uzun.");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("Posta kodu boş olamaz.")
                .MaximumLength(20).WithMessage("Posta kodu çok uzun.");
        }
    }
}
