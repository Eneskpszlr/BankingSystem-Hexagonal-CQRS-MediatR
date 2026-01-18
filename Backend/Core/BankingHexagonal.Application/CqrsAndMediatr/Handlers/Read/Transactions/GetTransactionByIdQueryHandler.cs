using AutoMapper;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Transactions
{
    public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, GetTransactionByIdQueryResult>
    {
        private readonly IGetTransactionByIdUseCase _useCase;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(IGetTransactionByIdUseCase useCase, IMapper mapper)
        {
            _useCase = useCase;
            _mapper = mapper;
        }
        public async Task<GetTransactionByIdQueryResult> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var t = await _useCase.ExecuteAsync(request.Id, request.UserId);

            return _mapper.Map<GetTransactionByIdQueryResult>(t);
        }
    }
}
