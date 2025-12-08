namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches
{
    public class GetBranchByIdQueryResult
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
    }
}
