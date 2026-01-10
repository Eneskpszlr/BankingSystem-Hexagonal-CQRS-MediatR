using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers;
using MediatR;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Customers
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerByIdQueryResult>
    {
        public int Id { get; set; }
        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
