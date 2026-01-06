namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Customers
{
    public class GetCustomerByIdQueryResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        // --- Address Value Object ---
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
