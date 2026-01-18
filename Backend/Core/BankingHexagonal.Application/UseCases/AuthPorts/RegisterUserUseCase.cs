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
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserUseCase(ICustomerRepository customerRepository, IAuthRepository authRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> ExecuteAsync(string firstName, string lastName, string tckn, DateTime birthDate, string password)
        {
            // 1. Müşteri Numarası ve Adres Hazırlığı
            var customerNumber = "ON" + new Random().Next(10000000, 99999999).ToString();
            var defaultAddress = new Address(
                street: "Adres Girilmedi",
                city: "Şehir Girilmedi",
                country: "Türkiye",
                zipCode: "00000"
            );

            // 2. Customer Entity Oluşturma
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

            // 3. Müşteriyi Veritabanına Kaydet
            await _customerRepository.CreateAsync(customer);
            await _unitOfWork.SaveChangesAsync();

            try
            {
                // 4. Identity User (AppUser) Hazırlığı
                var appUser = new AppUser
                {
                    UserName = tckn,
                    CustomerNumber = customerNumber,
                    CustomerId = customer.Id,
                };

                // 5. Kullanıcıyı Kaydet
                var registerResult = await _authRepository.RegisterUserAsync(appUser, password, "Customer");

                if (!registerResult.IsSuccess)
                {
                    _customerRepository.Delete(customer);
                    await _unitOfWork.SaveChangesAsync();

                    throw new Exception($"Kullanıcı oluşturulamadı: {registerResult.ErrorMessage}");
                }

                return "Kayıt Başarılı";
            }
            catch (Exception)
            {
                // Beklenmedik bir hata olursa da müşteriyi silmeye çalış
                if (customer.Id > 0)
                {
                    _customerRepository.Delete(customer);
                    await _unitOfWork.SaveChangesAsync();
                }
                throw;
            }
        }
    }
}
