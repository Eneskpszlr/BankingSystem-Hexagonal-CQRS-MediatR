using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.CqrsAndMediatr.Results.WriteResults
{
    public class BaseCommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? EntityId { get; set; }
        public List<string> Errors { get; set; }
    }
}
