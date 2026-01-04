namespace BankingHexagonal.Domain.Exceptions
{
    //Kendine transfer hatası
    public class SameAccountTransferException : BaseException
    {
        public SameAccountTransferException()
            : base("Gönderen ve alıcı hesap aynı olamaz.")
        {
        }
    }
}
