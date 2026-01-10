using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.Validators.Customers
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            // --- Kimlik Bilgileri ---
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Ad alanı 50 karakterden uzun olamaz.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz.")
                .MaximumLength(50).WithMessage("Soyad alanı 50 karakterden uzun olamaz.");

            // TCKN Validasyonu (Format)
            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("TC Kimlik No boş olamaz.")
                .Length(11).WithMessage("TC Kimlik No 11 haneli olmalıdır.")
                .Matches("^[0-9]*$").WithMessage("TC Kimlik No sadece rakamlardan oluşmalıdır.");

            // --- İletişim Bilgileri ---
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .MaximumLength(20).WithMessage("Telefon numarası çok uzun.");

            // --- Adres Bilgileri (Value Object Parçaları) ---
            RuleFor(x => x.Street).NotEmpty().WithMessage("Cadde/Sokak bilgisi gereklidir.");
            RuleFor(x => x.City).NotEmpty().WithMessage("Şehir bilgisi gereklidir.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("Ülke bilgisi gereklidir.");
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Posta kodu gereklidir.");
        }
    }
}
