using BankingHexagonal.Application.PrimaryPorts.AuthPorts;
using BankingHexagonal.Domain.Entities;
using BankingHexagonal.Domain.SecondaryPorts;
using BankingHexagonal.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankingHexagonal.Application.UseCases.AuthPorts
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAuthRepository _authRepository;

        public RegisterUserUseCase(ICustomerRepository customerRepository, IAuthRepository authRepository)
        {
            _customerRepository = customerRepository;
            _authRepository = authRepository;
        }

        public async Task<string> ExecuteAsync(string firstName, string lastName, string tckn, DateTime birthDate, string password)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var customerNumber = "ON" + new Random().Next(10000000, 99999999).ToString();
                    var defaultAddress = new Address(
                        street: "Adres Girilmedi",
                        city: "Şehir Girilmedi",
                        country: "Türkiye",
                        zipCode: "00000"
                    );

                    // Constructor Kullanımı
                    var customer = new Customer(
                        firstName: firstName,
                        lastName: lastName,
                        identityNumber: tckn,
                        customerNumber: customerNumber,
                        birthDate: birthDate,
                        email: $"{tckn}@onionbank.com",
                        phone: "",
                        address: defaultAddress
                    );

                    var createdCustomer = await _customerRepository.AddAsync(customer);

                    // 2. Kullanıcı (Identity) Oluştur
                    var appUser = new AppUser
                    {
                        UserName = tckn,
                        CustomerNumber = customerNumber,
                        CustomerId = createdCustomer.Id,
                        Customer = createdCustomer
                    };

                    var registerResult = await _authRepository.RegisterUserAsync(appUser, password, "Customer");

                    if (!registerResult.IsSuccess)
                    {
                        throw new Exception($"Kayıt başarısız: {registerResult.ErrorMessage}");
                    }

                    scope.Complete();
                    return "Kayıt Başarılı";
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
