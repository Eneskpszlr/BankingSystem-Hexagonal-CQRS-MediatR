namespace BankingHexagonal.Domain.Exceptions
{
    // Geçersiz Tutar Hatası
    public class InvalidAmountException : BaseException
    {
        public InvalidAmountException() : base("Tutar sıfırdan büyük olmalıdır.") { }
    }
}
