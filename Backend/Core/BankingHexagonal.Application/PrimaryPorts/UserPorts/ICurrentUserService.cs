using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.PrimaryPorts.UserPorts
{
    public interface ICurrentUserService
    {
        int GetUserId();
    }
}
