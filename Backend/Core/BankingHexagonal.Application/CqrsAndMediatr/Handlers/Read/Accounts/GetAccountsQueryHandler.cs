using AutoMapper;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.SecondaryPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Accounts
{
    public class GetAccountsQueryHandler : IRequestHandler<GetAccountsQuery, List<GetAccountsQueryResult>>
    {
        private readonly IGetAccountsUseCase _useCase;
        private readonly IMapper _mapper;

        public GetAccountsQueryHandler(IGetAccountsUseCase useCase, IMapper mapper)
        {
            _useCase = useCase;
            _mapper = mapper;
        }

        public async Task<List<GetAccountsQueryResult>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await _useCase.ExecuteAsync(request.UserId, request.IsAdmin);

            return _mapper.Map<List<GetAccountsQueryResult>>(accounts);
        }
    }
}
