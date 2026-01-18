using AutoMapper;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Customers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdQueryResult?>
    {
        private readonly IGetCustomerByIdUseCase _useCase;
        private readonly IMapper _mapper;
        public GetCustomerByIdQueryHandler(IGetCustomerByIdUseCase useCase, IMapper mapper)
        {
            _useCase = useCase;
            _mapper = mapper;
        }
        public async Task<GetCustomerByIdQueryResult> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _useCase.ExecuteAsync(request.Id, request.UserId, request.IsAdmin);

            return _mapper.Map<GetCustomerByIdQueryResult>(customer);
        }
    }
}
