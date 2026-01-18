using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers;
using MediatR;
using System.Text.Json.Serialization;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Customers
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerByIdQueryResult>
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
