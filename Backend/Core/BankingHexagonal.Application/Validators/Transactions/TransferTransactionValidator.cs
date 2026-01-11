using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Transactions
{
    public class TransferTransactionValidator : AbstractValidator<TransferTransactionCommand>
    {
        public TransferTransactionValidator()
        {
            // Gönderen Hesap
            RuleFor(x => x.FromAccountId)
                .GreaterThan(0).WithMessage("Gönderen Hesap ID geçersiz.");

            // Alıcı Hesap
            RuleFor(x => x.ToAccountNumber)
                .NotEmpty().WithMessage("Alıcı Hesap Numarası boş olamaz.")
                .MinimumLength(5).WithMessage("Hesap numarası çok kısa.");

            // Tutar
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Transfer tutarı 0'dan büyük olmalıdır.");

            // Para Birimi
            RuleFor(x => x.CurrencyCode)
                .NotEmpty().WithMessage("Para birimi boş olamaz.")
                .Length(3).WithMessage("Para birimi 3 karakter olmalıdır.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir.");
        }
    }
}
