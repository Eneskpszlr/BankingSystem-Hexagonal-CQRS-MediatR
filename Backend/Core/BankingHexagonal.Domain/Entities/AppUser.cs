using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string CustomerNumber { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
