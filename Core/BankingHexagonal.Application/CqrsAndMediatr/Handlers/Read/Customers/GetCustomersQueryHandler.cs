using BankingHexagonal.Application.CqrsAndMediatr.Queries.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Customers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, List<GetCustomersQueryResult>>
    {
        private readonly IGetCustomersUseCase _useCase;

        public GetCustomersQueryHandler(IGetCustomersUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<List<GetCustomersQueryResult>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _useCase.ExecuteAsync();

            return customers.Select(c => new GetCustomersQueryResult
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                IdentityNumber = c.IdentityNumber,
                Email = c.Email,
                Phone = c.Phone,

                // --- VALUE OBJECT MAPPING ---
                Street = c.Address.Street,
                City = c.Address.City,
                Country = c.Address.Country,
                ZipCode = c.Address.ZipCode
            }).ToList();
        }
    }
}
