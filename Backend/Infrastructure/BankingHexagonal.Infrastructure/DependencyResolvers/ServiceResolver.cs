using BankingHexagonal.Application.PrimaryPorts.AuthPorts;
using BankingHexagonal.Application.PrimaryPorts.UserPorts;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Infrastructure.DependencyResolvers
{
    public static class ServiceResolver
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
