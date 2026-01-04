using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Accounts
{
    public class UpdateAccountValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountValidator()
        {
            // Güncellenecek kaydın ID'si
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Güncellenecek hesabın ID bilgisi geçersiz.");

            // yeni şube ID kontrolü
            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("Geçerli bir Şube ID girilmelidir.");

            // Hesap numarası kontrolü
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Hesap numarası boş bırakılamaz.")
                .MinimumLength(10).WithMessage("Hesap numarası çok kısa.");
        }
    }
}
