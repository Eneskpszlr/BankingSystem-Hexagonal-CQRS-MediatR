using BankingHexagonal.Domain.Entities.Base;
using BankingHexagonal.Domain.Enums;
using BankingHexagonal.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankingHexagonal.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string IdentityNumber { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public Address Address { get; private set; }

        private readonly List<Account> _accounts = new();
        public virtual IReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();

        protected Customer() { }

        public Customer(string firstName, string lastName, string identityNumber, string email, Address address, string phone)
        {
            if (string.IsNullOrWhiteSpace(firstName)) 
                throw new ArgumentException("Ad alanı boş olamaz.");
            if (string.IsNullOrWhiteSpace(lastName)) 
                throw new ArgumentException("Soyad alanı boş olamaz.");
            if (string.IsNullOrWhiteSpace(identityNumber))
                throw new ArgumentException("TCKN boş olamaz.");
            if (string.IsNullOrWhiteSpace(email)) 
                throw new ArgumentException("E-posta boş olamaz.");
            if (string.IsNullOrWhiteSpace(phone)) 
                throw new ArgumentException("Telefon boş olamaz.");

            // Address null gelirse hata fırlat
            if (address == null) throw new ArgumentNullException(nameof(address));

            FirstName = firstName;
            LastName = lastName;
            IdentityNumber = identityNumber;
            Email = email;
            Address = address;
            Phone = phone;

            CreatedDate = DateTime.UtcNow;
            Status = DataStatus.Inserted;
        }

        // Adres Güncelleme Metodu
        public void UpdateAddress(Address newAddress)
        {
            Address = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
            UpdatedDate = DateTime.UtcNow;
            Status = DataStatus.Updated;
        }

        public void UpdateName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("İsim/Soyisim boş olamaz.");
            FirstName = firstName;
            LastName = lastName;
            UpdatedDate = DateTime.UtcNow;
            Status = DataStatus.Updated;
        }

        public void UpdateContactInfo(string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(phone)) 
                throw new ArgumentException("Telefon boş olamaz.");
            if (string.IsNullOrWhiteSpace(email)) 
                throw new ArgumentException("E-posta boş olamaz.");

            Phone = phone;
            Email = email;
            UpdatedDate = DateTime.UtcNow;
            Status = DataStatus.Updated;
        }
    }
}
