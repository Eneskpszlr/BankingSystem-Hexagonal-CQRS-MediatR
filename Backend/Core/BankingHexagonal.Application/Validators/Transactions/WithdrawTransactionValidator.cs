using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Transactions
{
    public class WithdrawTransactionValidator : AbstractValidator<WithdrawTransactionCommand>
    {
        public WithdrawTransactionValidator()
        {
            RuleFor(x => x.AccountId)
                .GreaterThan(0).WithMessage("Geçerli bir Hesap ID girilmelidir.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Çekilecek tutar 0'dan büyük olmalıdır.");

            RuleFor(x => x.CurrencyCode)
                .NotEmpty().WithMessage("Para birimi boş olamaz.")
                .Length(3).WithMessage("Para birimi 3 karakter olmalıdır.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir.");
        }
    }
}
