using BankingHexagonal.Persistence.EFData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Persistence.DependencyResolvers
{
    public static class DbContextResolver
    {
        public static IServiceCollection AddDbContextServices(this IServiceCollection services)
        {
            services.AddDbContext<MyContext>((serviceProvider, opt) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                opt.UseSqlServer(config.GetConnectionString("MyConnection"));
            });

            return services;
        }
    }
}
