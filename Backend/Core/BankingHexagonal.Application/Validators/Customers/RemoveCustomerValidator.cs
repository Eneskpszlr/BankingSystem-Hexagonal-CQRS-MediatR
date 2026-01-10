using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Customers
{
    public class RemoveCustomerValidator : AbstractValidator<RemoveCustomerCommand>
    {
        public RemoveCustomerValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Silinecek müşterinin ID bilgisi geçersiz.")
                .NotNull().WithMessage("ID alanı zorunludur.");
        }
    }
}
