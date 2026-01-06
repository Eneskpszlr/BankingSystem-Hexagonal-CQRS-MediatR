namespace BankingHexagonal.Domain.Exceptions
{
    //Transferde Hedef hesap zorunluluğu
    public class TargetAccountRequiredException : BaseException
    {
        public TargetAccountRequiredException()
            : base("Transfer işlemi için hedef hesap belirtilmelidir.")
        {
        }
    }
}
