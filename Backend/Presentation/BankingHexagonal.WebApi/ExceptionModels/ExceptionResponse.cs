using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.WebApi.ExceptionModels
{
    public class ExceptionResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        public List<string> Errors { get; set; }
    }
}
