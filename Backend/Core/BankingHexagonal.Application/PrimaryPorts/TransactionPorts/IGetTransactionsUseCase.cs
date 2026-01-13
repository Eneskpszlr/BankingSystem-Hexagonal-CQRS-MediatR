using BankingHexagonal.Application.CqrsAndMediatr.Queries.Transactions;
using BankingHexagonal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.PrimaryPorts.TransactionPorts
{
    public interface IGetTransactionsUseCase
    {
        Task<List<Transaction>> ExecuteAsync(GetTransactionsQuery query);
    }
}
