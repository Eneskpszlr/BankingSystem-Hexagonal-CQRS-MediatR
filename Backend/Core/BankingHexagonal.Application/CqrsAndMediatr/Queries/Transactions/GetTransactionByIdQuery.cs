using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions;
using MediatR;
using System.Text.Json.Serialization;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions
{
    public class GetTransactionByIdQuery : IRequest<GetTransactionByIdQueryResult>
    {
        public GetTransactionByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
    }
}
