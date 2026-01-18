using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Queries.Customers
{
    public class GetCustomersQuery : IRequest<List<GetCustomersQueryResult>>
    {
    }
}
