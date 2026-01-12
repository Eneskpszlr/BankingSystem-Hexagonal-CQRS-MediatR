namespace BankingHexagonal.Domain.Exceptions
{
    public class DomainException : BaseException
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
