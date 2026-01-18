using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using MediatR;
using System.Text.Json.Serialization;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Accounts
{
    public class GetAccountByIdQuery : IRequest<GetAccountByIdQueryResult>
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int Id { get; set; }
        public GetAccountByIdQuery(int id)
        {
            Id = id;
        }
    }
}
