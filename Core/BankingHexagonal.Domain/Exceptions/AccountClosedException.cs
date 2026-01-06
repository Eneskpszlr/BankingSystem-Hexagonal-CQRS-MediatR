namespace BankingHexagonal.Domain.Exceptions
{
    // Hesap Pasif/Kapalı Hatası
    public class AccountClosedException : BaseException
    {
        public AccountClosedException() : base("Hesap kapalı olduğu için işlem yapılamaz.") { }
    }
}
