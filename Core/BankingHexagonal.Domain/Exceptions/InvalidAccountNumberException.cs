namespace BankingHexagonal.Domain.Exceptions
{
    public class InvalidAccountNumberException : BaseException
    {
        public InvalidAccountNumberException()
            : base("Account number cannot be empty.")
        {
        }
    }

}
