namespace BankingHexagonal.Domain.Exceptions
{
    //Para birimi eşleşmeme hatası
    public class CurrencyMismatchException : BaseException
    {
        public CurrencyMismatchException(string accountCurrency, string transactionCurrency)
            : base($"Hesap para birimi ({accountCurrency}) ile işlem para birimi ({transactionCurrency}) uyuşmuyor.")
        {
        }
    }
}
