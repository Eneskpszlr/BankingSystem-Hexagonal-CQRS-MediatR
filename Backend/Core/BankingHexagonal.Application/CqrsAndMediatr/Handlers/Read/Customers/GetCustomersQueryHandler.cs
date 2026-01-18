using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IGetCustomersUseCase useCase, IMapper mapper)
        {
            _useCase = useCase;
            _mapper = mapper;
        }

        public async Task<List<GetCustomersQueryResult>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _useCase.ExecuteAsync();

            return _mapper.Map<List<GetCustomersQueryResult>>(customers);
        }
    }
}
