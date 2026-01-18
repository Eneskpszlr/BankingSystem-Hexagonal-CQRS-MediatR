using AutoMapper;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Accounts;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers;
using BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Transactions;
using BankingHexagonal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 1. ACCOUNT MAPPING
            CreateMap<Account, GetAccountByIdQueryResult>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance.Amount)) // Money.Amount -> decimal
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Balance.Currency)) // Money.Currency -> string
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString())); // Enum -> string

            CreateMap<Account, GetAccountsQueryResult>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance.Amount))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Balance.Currency))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));


            // 2. CUSTOMER MAPPING
            CreateMap<Customer, GetCustomerByIdQueryResult>()
                // Address Value Object Flattening (Düzleştirme)
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode));

            // Listeleme DTO'su için de aynısı
            CreateMap<Customer, GetCustomersQueryResult>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode));


            // 3. BRANCH MAPPING
            CreateMap<Branch, GetBranchByIdQueryResult>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode));

            CreateMap<Branch, GetBranchesQueryResult>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode));


            // 4. TRANSACTION MAPPING
            CreateMap<Transaction, GetTransactionByIdQueryResult>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Amount))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Amount.Currency));

            CreateMap<Transaction, GetTransactionsQueryResult>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.TransactionType.ToString()))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Amount))
                .ForMember(dest => dest.CurrencyCode, opt => opt.MapFrom(src => src.Amount.Currency));
        }
    }
}
