using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Branches
{
    public class GetBranchesQuery : IRequest<List<GetBranchesQueryResult>>
    {
    }
}
