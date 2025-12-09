using BankingHexagonal.Application.CqrsAndMediatr.Queries.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Customers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdQueryResult?>
    {
        private readonly IGetCustomerByIdUseCase _useCase;
        public GetCustomerByIdQueryHandler(IGetCustomerByIdUseCase useCase)
        {
            _useCase = useCase;
        }
        public async Task<GetCustomerByIdQueryResult?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _useCase.ExecuteAsync(request.Id);

            return new GetCustomerByIdQueryResult
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                IdentityNumber = customer.IdentityNumber,
                Address = customer.Address,
                Phone = customer.Phone,
                Email = customer.Email
            };
        }
    }
}
