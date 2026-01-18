using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Customers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerCommandResult>
    {
        private readonly IUpdateCustomerUseCase _useCase;
        public UpdateCustomerCommandHandler(IUpdateCustomerUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<UpdateCustomerCommandResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _useCase.ExecuteAsync(request);

            return new UpdateCustomerCommandResult
            {
                Success = true,
                Message = "Müşteri başarıyla güncellendi."
            };
        }
    }
}
