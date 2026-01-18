using AutoMapper;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Accounts
{
    public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, GetAccountByIdQueryResult>
    {
        private readonly IGetAccountByIdUseCase _useCase;
        private readonly IMapper _mapper;
        public GetAccountByIdQueryHandler(IGetAccountByIdUseCase useCase, IMapper mapper)
        {
            _useCase = useCase;
            _mapper = mapper;
        }
        public async Task<GetAccountByIdQueryResult> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _useCase.ExecuteAsync(request.Id, request.UserId);

            return _mapper.Map<GetAccountByIdQueryResult>(account);
        }
    }
}
