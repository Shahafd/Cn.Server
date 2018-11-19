using CN.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IRepositories
{
    public interface INetworkRepository
    {
        User GetUserByUsername(string username);
    }
}
