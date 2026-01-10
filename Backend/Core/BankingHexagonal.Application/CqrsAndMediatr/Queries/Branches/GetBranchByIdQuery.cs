using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Branches
{
    public class GetBranchByIdQuery : IRequest<GetBranchByIdQueryResult>
    {
        public GetBranchByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
