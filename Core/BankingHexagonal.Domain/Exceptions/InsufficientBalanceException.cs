namespace BankingHexagonal.Domain.Exceptions
{
    // Bakiye Yetersiz Hatası
    public class InsufficientBalanceException : BaseException
    {
        public InsufficientBalanceException() : base("İşlem için bakiye yetersiz.") { }
    }
}
