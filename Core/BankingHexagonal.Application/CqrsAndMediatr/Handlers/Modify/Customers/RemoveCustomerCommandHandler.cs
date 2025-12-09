using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Customers
{
    public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand, RemoveCustomerCommandResult>
    {
        private readonly IRemoveCustomerUseCase _useCase;
        public RemoveCustomerCommandHandler(IRemoveCustomerUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<RemoveCustomerCommandResult> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            await _useCase.ExecuteAsync(request.Id);
            return new RemoveCustomerCommandResult
            {
                Message = "Müşteri başarıyla silindi.",
            };
        }
    }
}
