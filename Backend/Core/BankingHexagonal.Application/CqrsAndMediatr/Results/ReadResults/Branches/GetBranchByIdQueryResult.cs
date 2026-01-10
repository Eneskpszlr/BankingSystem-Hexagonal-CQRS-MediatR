namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches
{
    public class GetBranchByIdQueryResult
    {
        public int Id { get; set; }
        public string BranchName { get; set; }

        // --- Address Value Object (Flattened) ---
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
