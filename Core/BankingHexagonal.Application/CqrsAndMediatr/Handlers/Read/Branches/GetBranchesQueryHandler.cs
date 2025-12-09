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

        public GetBranchesQueryHandler(IGetBranchesUseCase useCase)
        {
            _useCase = useCase;
        }

        public async Task<List<GetBranchesQueryResult>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
        {
            var branches = await _useCase.ExecuteAsync();
            return branches.Select(b => new GetBranchesQueryResult
            {
                Id = b.Id,
                BranchName = b.BranchName,
                Address = b.Address
            }).ToList();
        }
    }
}
