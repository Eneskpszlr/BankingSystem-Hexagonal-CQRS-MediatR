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
        public async Task<GetCustomerByIdQueryResult> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _useCase.ExecuteAsync(request.Id);

            return new GetCustomerByIdQueryResult
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                IdentityNumber = customer.IdentityNumber,
                Email = customer.Email,
                Phone = customer.Phone,

                // --- VALUE OBJECT MAPPING ---
                Street = customer.Address.Street,
                City = customer.Address.City,
                Country = customer.Address.Country,
                ZipCode = customer.Address.ZipCode
            };
        }
    }
}
