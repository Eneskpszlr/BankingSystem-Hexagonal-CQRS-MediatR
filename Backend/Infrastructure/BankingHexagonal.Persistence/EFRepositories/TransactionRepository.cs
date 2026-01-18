using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFData;
using Microsoft.EntityFrameworkCore;

namespace BankingHexagonal.Persistence.EFRepositories
{
    public class TransactionRepository(MyContext context) : BaseRepository<Transaction>(context), ITransactionRepository
    {
        public async Task<List<Transaction>> GetFilteredAsync(int userId, int? accountId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Transactions
                .Include(t => t.Account)
                .AsQueryable();

            query = query.Where(t => t.Account.CustomerId == userId);

            // 1. Hesap Filtresi (Varsa)
            if (accountId.HasValue && accountId.Value > 0)
            {
                // İşlem bu hesaba aitse (Gönderen veya Alan)
                query = query.Where(t => t.AccountId == accountId || t.TargetAccountId == accountId);
            }

            // 2. Başlangıç Tarihi
            if (startDate.HasValue)
            {
                query = query.Where(t => t.CreatedDate >= startDate.Value);
            }

            // 3. Bitiş Tarihi
            if (endDate.HasValue)
            {
                // O günün sonuna kadar (23:59:59)
                var end = endDate.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(t => t.CreatedDate <= end);
            }

            // 4. Veriyi Çek
            return await query
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedDate)
                .Take(50)
                .ToListAsync();
        }
    }
}
