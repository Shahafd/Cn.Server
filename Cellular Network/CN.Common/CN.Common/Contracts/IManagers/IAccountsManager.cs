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
        User UserLogin(UserLogin userLogin);
        string UpdateExisitngClient(Client client);
        List<string> AddNewClient(Client client);
        List<Client> GetAllClients();
        bool IsClientIdExists(string clientId);
        string DeleteClient(string id);
        List<Client> SearchForClients(string input);
        Client ClientLogin(ClientLogin clientLogin);
        List<Client> GetMostValueClients();
        List<Client> GetMostCallingToCenter();
        List<User> GetBestSellers();
    }
}
