namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers
{
    public class GetCustomerByIdQueryResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
