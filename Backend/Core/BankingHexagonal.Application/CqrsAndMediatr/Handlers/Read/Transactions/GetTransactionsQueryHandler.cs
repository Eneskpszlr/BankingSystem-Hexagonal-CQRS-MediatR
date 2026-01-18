using AutoMapper;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Transactions
{
    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<GetTransactionsQueryResult>>
    {
        private readonly IGetTransactionsUseCase _useCase;
        private readonly IMapper _mapper;

        public GetTransactionsQueryHandler(IGetTransactionsUseCase useCase, IMapper mapper)
        {
            _useCase = useCase;
            _mapper = mapper;
        }

        public async Task<List<GetTransactionsQueryResult>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _useCase.ExecuteAsync(request);
            return _mapper.Map<List<GetTransactionsQueryResult>>(transactions);
        }
    }
}
