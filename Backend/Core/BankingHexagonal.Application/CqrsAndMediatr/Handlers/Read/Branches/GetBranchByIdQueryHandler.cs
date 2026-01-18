using AutoMapper;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Branches
{
    public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, GetBranchByIdQueryResult>
    {
        private readonly IGetBranchByIdUseCase _useCase;
        private readonly IMapper _mapper;
        public GetBranchByIdQueryHandler(IGetBranchByIdUseCase useCase, IMapper mapper)
        {
            _useCase = useCase;
            _mapper = mapper;
        }
        public async Task<GetBranchByIdQueryResult> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            var branch = await _useCase.ExecuteAsync(request.Id);

            return _mapper.Map<GetBranchByIdQueryResult>(branch);
        }
    }
}
