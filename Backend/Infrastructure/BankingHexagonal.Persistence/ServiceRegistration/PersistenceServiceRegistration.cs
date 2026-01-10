using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Persistence.EFData;
using BankingHexagonal.Persistence.EFRepositories;
using BankingHexagonal.Persistence.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Persistence.ServiceRegistration
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Interceptor'ı DI Container'a ekle
            services.AddScoped<AuditableEntityInterceptor>();

            // 2. DbContext Ayarları
            services.AddDbContext<MyContext>((serviceProvider, options) =>
            {
                // Interceptor'ı ServiceProvider'dan çekiyoruz
                var interceptor = serviceProvider.GetRequiredService<AuditableEntityInterceptor>();

                options.UseSqlServer(configuration.GetConnectionString("MyConnection"))
                       .AddInterceptors(interceptor);
            });

            // 3. Repositories (Specific Repositories)
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();

            // 4. UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
