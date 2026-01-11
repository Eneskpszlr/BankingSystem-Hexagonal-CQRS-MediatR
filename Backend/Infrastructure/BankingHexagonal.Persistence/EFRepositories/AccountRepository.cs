using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFData;
using Microsoft.EntityFrameworkCore;

namespace BankingHexagonal.Persistence.EFRepositories
{
    public class AccountRepository(MyContext context) : BaseRepository<Account>(context), IAccountRepository
    {
        // Standart dışı, özel metod
        public async Task<Account> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        // Örn: Sadece belirli bir numaraya göre hesap bulma
        public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
        }
    }
}
