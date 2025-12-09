using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Accounts
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, UpdateAccountCommandResult>
    {
        private readonly IUpdateAccountUseCase _useCase;

        public UpdateAccountCommandHandler(IUpdateAccountUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<UpdateAccountCommandResult> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            await _useCase.ExecuteAsync(request);

            return new UpdateAccountCommandResult
            {
                Message = "Hesap başarıyla güncellendi."
            };
        }
    }
}
