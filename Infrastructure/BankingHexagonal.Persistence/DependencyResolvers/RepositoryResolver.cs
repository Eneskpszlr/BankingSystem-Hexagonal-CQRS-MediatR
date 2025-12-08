using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace BankingHexagonal.Persistence.DependencyResolvers
{
    public static class RepositoryResolver
    {
        public static void AddRepositoryService(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
        }
    }
}
