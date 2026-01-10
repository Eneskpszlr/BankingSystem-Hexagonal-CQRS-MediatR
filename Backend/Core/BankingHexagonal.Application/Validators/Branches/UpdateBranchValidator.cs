using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Branches
{
    public class UpdateBranchValidator : AbstractValidator<UpdateBranchCommand>
    {
        public UpdateBranchValidator()
        {
            // ID Kontrolü
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Güncellenecek şubenin ID bilgisi geçersiz.");

            // Şube Adı
            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("Şube adı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Şube adı çok uzun.");

            // Adres Detayları (Hepsi dolu olmalı)
            RuleFor(x => x.Street).NotEmpty().WithMessage("Cadde/Sokak bilgisi gerekli.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir bilgisi gerekli.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Ülke bilgisi gerekli.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Posta kodu gerekli.");
        }
    }
}
