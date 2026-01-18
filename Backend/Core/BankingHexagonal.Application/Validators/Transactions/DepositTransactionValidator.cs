using BankingHexagonal.Application.CqrsAndMediatr.Commands.Transactions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.Validators.Transactions
{
    public class DepositTransactionValidator : AbstractValidator<DepositTransactionCommand>
    {
        public DepositTransactionValidator()
        {
            // Hesap Kontrolü
            RuleFor(x => x.AccountId)
                .GreaterThan(0).WithMessage("Geçerli bir Hesap ID girilmelidir.");

            // Tutar Kontrolü (0 veya negatif olamaz)
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Yatırılacak tutar 0'dan büyük olmalıdır.");

            // Para Birimi
            RuleFor(x => x.CurrencyCode)
                .NotEmpty().WithMessage("Para birimi boş olamaz.")
                .Length(3).WithMessage("Para birimi 3 karakter olmalıdır (Örn: TRY).");

            // Açıklama
            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Açıklama en fazla 200 karakter olabilir.");
        }
    }
}
