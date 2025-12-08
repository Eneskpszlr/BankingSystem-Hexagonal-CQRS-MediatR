using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;

namespace BankingHexagonal.Application.UseCases.Accounts
{
    public class GetAccountByIdUseCase : IGetAccountByIdUseCase
    {
        private readonly IAccountRepository _repository;
        public GetAccountByIdUseCase(IAccountRepository repository)
        {
            _repository = repository;
        }
        public async Task<Account> ExecuteAsync(int id)
        {
            var exist = await _repository.GetByIdAsync(id);
            if (exist == null)
                throw new Exception("Hesap bulunamadı.");
            return exist;
        }
    }
}
