using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Accounts
{
    public class DeleteAccountValidator : AbstractValidator<RemoveAccountCommand>
    {
        public DeleteAccountValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Silinecek hesabın ID bilgisi geçersiz.")
                .NotNull().WithMessage("ID boş olamaz.");
        }
    }
}
