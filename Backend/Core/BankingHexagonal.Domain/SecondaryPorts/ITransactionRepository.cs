using BankingHexagonal.Domain.Entities;

namespace BankingHexagonal.Domain.SecondaryPorts
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> GetFilteredAsync(int userId, int? accountId, DateTime? startDate, DateTime? endDate);
    }
}
