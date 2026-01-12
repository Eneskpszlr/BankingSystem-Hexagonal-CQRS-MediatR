using BankingHexagonal.Domain.Exceptions;

namespace BankingHexagonal.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        private Address() { } // EF Core için

        public Address(string street, string city, string country, string zipCode)
        {
            // Validasyonlar burada yapılır! Entity temiz kalır.
            if (string.IsNullOrWhiteSpace(street))
                throw new DomainException("Sokak bilgisi boş olamaz.");

            if (string.IsNullOrWhiteSpace(city))
                throw new DomainException("Şehir bilgisi boş olamaz.");

            Street = street;
            City = city;
            Country = country;
            ZipCode = zipCode;
        }



        // Eşitlik kontrolü için hangi alanlara bakılacağını söylüyoruz
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
            yield return ZipCode;
        }
    }
}
