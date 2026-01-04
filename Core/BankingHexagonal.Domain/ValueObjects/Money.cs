using BankingHexagonal.Domain.Exceptions;

namespace BankingHexagonal.Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        private Money() { }

        public Money(decimal amount, string currency)
        {
            if (amount < 0) 
                throw new InvalidAmountException(); // Negatif para olamaz

            Amount = amount;
            Currency = currency;
        }

        // Toplama işlemi: 100 USD + 50 USD = 150 USD
        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new CurrencyMismatchException(Currency, other.Currency);

            return new Money(Amount + other.Amount, Currency);
        }

        // Çıkarma işlemi
        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new CurrencyMismatchException(Currency, other.Currency);

            // Eksiye düşme kontrolünü burada yapabilirsiz veya Account'a bırakabiliriz.
            return new Money(Amount - other.Amount, Currency);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
