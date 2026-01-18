using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Accounts
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountValidator()
        {
            // 1. Müşteri Kontrolü
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Geçerli bir Müşteri ID girilmelidir.")
                .NotNull().WithMessage("Müşteri ID boş olamaz.");

            // 2. Şube Kontrolü
            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("Geçerli bir Şube ID girilmelidir.");

            // 3. Hesap Numarası Kontrolürmat)
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Hesap numarası boş olamaz.")
                .MinimumLength(10).WithMessage("Hesap numarası en az 10 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Hesap numarası en fazla 20 karakter olabilir.");
            //  Regex ile sadece rakam olmasını istersek:
            // .Matches("^[0-9]*$").WithMessage("Hesap numarası sadece rakamlardan oluşmalıdır.");

            // 4. Para Birimi Kontrolü
            RuleFor(x => x.CurrencyCode)
                .NotEmpty().WithMessage("Para birimi seçilmelidir.")
                .Length(3).WithMessage("Para birimi 3 karakter olmalıdır (Örn: TRY, USD).")
                .Must(x => x == "TRY" || x == "USD" || x == "EUR" ||x == "GBP" )
                .WithMessage("Sadece TRY, USD, EUR veya GBP cinsinden hesap açılabilir.");
        }
    }
}
