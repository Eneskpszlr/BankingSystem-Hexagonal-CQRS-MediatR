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
            RuleFor(x => x.ToAccountId)
                .GreaterThan(0).WithMessage("Alıcı Hesap ID geçersiz.")
                // KENDİNE TRANSFER ENGELİ
                .NotEqual(x => x.FromAccountId).WithMessage("Gönderen ve Alıcı hesap aynı olamaz.");

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
