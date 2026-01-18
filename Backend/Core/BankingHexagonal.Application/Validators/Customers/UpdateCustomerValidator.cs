using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Customers
{
    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerValidator()
        {
            // ID Kontrolü
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Güncellenecek müşterinin ID bilgisi geçersiz.");

            // İsim Güncellemesi
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);

            // İletişim Güncellemesi
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);

            // Adres Güncellemesi
            RuleFor(x => x.Street).NotEmpty().WithMessage("Cadde/Sokak bilgisi gereklidir.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir bilgisi gereklidir.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Ülke bilgisi gereklidir.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Posta kodu gereklidir.");
        }
    }
}
