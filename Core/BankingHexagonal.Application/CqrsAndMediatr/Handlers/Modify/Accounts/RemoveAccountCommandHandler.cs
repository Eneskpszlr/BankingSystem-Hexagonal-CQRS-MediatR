using BankingHexagonal.Application.CqrsAndMediatr.Commands.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Accounts
{
    public class RemoveAccountCommandHandler : IRequestHandler<RemoveAccountCommand, RemoveAccountCommandResult>
    {
        private readonly IRemoveAccountUseCase _useCase;
        public RemoveAccountCommandHandler(IRemoveAccountUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<RemoveAccountCommandResult> Handle(RemoveAccountCommand request, CancellationToken cancellationToken)
        {
            await _useCase.ExecuteAsync(request.Id);
            return new RemoveAccountCommandResult
            {
                Success = true,
                Message = "Hesap başarıyla silindi."
            };
        }
    }
}
