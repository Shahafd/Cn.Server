using CN.Common.Enums;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IManagers
{
    public interface IAccountsManager
    {
        Tuple<User, RequestStatusEnum> UserLogin(UserLogin userLogin);
       RequestStatusEnum UpdateExisitngClient(Client client);
       string AddNewClient(Client client);
    }
}
