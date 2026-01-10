using BankingHexagonal.Application.CqrsAndMediatr.Commands.Branches;
using FluentValidation;

namespace BankingHexagonal.Application.Validators.Branches
{
    public class RemoveBranchValidator : AbstractValidator<RemoveBranchCommand>
    {
        public RemoveBranchValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Silinecek şubenin ID bilgisi geçersiz.")
                .NotNull().WithMessage("ID boş olamaz.");
        }
    }
}
