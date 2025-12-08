using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts
{
    public class GetAccountByIdQuery : IRequest<GetAccountByIdQueryResult>
    {
        public int Id { get; set; }
        public GetAccountByIdQuery(int id)
        {
            Id = id;
        }
    }
}
