using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CN.Common.Models;
using CN.Common.Models.TempModels;

namespace CN.Common.Contracts
{
    public interface IAccountsRepository
    {
        User GetUserByUsername(string username);
    }
}
