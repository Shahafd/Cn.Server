using CN.Common.Enums;
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
        Client GetClientByID(string iD);
        RequestStatusEnum UpdateClientDetails(Client client);
        RequestStatusEnum AddNewClient(Client client);
        bool IsClientIdExisits(string iD);
    }
}
