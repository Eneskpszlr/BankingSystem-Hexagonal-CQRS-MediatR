using BankingHexagonal.Application.PrimaryPorts.AccountPorts;
using BankingHexagonal.Application.PrimaryPorts.AuthPorts;
using BankingHexagonal.Application.PrimaryPorts.BranchPorts;
using BankingHexagonal.Application.PrimaryPorts.CustomerPorts;
using BankingHexagonal.Application.PrimaryPorts.TransactionPorts;
using BankingHexagonal.Application.UseCases.Accounts;
using BankingHexagonal.Application.UseCases.AuthPorts;
using BankingHexagonal.Application.UseCases.Branches;
using BankingHexagonal.Application.UseCases.Customers;
using BankingHexagonal.Application.UseCases.Transactions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.DependencyResolvers
{
    public static class ApplicationResolver
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
            services.AddScoped<IUpdateCustomerUseCase, UpdateCustomerUseCase>();
            services.AddScoped<IRemoveCustomerUseCase, RemoveCustomerUseCase>();
            services.AddScoped<IGetCustomerByIdUseCase, GetCustomerByIdUseCase>();
            services.AddScoped<IGetCustomersUseCase, GetCustomersUseCase>();

            services.AddScoped<ICreateAccountUseCase, CreateAccountUseCase>();
            services.AddScoped<IUpdateAccountUseCase, UpdateAccountUseCase>();
            services.AddScoped<IRemoveAccountUseCase, RemoveAccountUseCase>();
            services.AddScoped<IGetAccountByIdUseCase, GetAccountByIdUseCase>();
            services.AddScoped<IGetAccountsUseCase, GetAccountsUseCase>();

            services.AddScoped<ICreateBranchUseCase, CreateBranchUseCase>();
            services.AddScoped<IGetBranchByIdUseCase, GetBranchByIdUseCase>();
            services.AddScoped<IUpdateBranchUseCase, UpdateBranchUseCase>();
            services.AddScoped<IRemoveBranchUseCase, RemoveBranchUseCase>();
            services.AddScoped<IGetBranchesUseCase, GetBranchesUseCase>();

            services.AddScoped<IDepositUseCase, DepositTransactionUseCase>();
            services.AddScoped<IGetTransactionByIdUseCase, GetTransactionByIdUseCase>();
            services.AddScoped<IWithdrawUseCase, WithdrawTransactionUseCase>();
            services.AddScoped<ITransferUseCase, TransferTransactionUseCase>();
            services.AddScoped<IGetTransactionsUseCase, GetTransactionsUseCase>();

            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();

            return services;

        }
    }
}