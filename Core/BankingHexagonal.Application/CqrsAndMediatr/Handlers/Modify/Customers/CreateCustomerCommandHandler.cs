using BankingHexagonal.Application.CqrsAndMediatr.Commands.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Modify.Customers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResult>
    {
        private readonly ICreateCustomerUseCase _useCase;

        public CreateCustomerCommandHandler(ICreateCustomerUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<CreateCustomerCommandResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // UseCase'den ID'yi alıyoruz
            int createdCustomerId = await _useCase.ExecuteAsync(request);

            return new CreateCustomerCommandResult
            {
                Success = true,
                Message = "Müşteri başarıyla oluşturuldu.",
                EntityId = createdCustomerId // Oluşan ID'yi dönüyoruz
            };
        }
    }
}
