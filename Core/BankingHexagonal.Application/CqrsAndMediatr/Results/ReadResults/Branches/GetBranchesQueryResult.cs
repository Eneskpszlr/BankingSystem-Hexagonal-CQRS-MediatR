using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Results.ReadResults.Branches
{
    public class GetBranchesQueryResult
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
    }
}
