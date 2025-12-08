using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions
{
    public class GetTransactionByIdQuery : IRequest<GetTransactionByIdQuery>
    {
        public GetTransactionByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
