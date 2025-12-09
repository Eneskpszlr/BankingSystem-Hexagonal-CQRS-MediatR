using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions
{
    public class GetTransactionByIdQuery : IRequest<GetTransactionByIdQueryResult>
    {
        public GetTransactionByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
