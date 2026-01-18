using AutoMapper;
using BankingHexagonal.Application.CqrsAndMediatr.Queries.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Handlers.Read.Branches
{
    public class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, List<GetBranchesQueryResult>>
    {
        private readonly IGetBranchesUseCase _useCase;
        private readonly IMapper _mapper;

        public GetBranchesQueryHandler(IGetBranchesUseCase useCase, IMapper mapper)
        {
            _useCase = useCase;
            _mapper = mapper;
        }

        public async Task<List<GetBranchesQueryResult>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
        {
            var branches = await _useCase.ExecuteAsync();

            return _mapper.Map<List<GetBranchesQueryResult>>(branches); 
        }
    }
}
