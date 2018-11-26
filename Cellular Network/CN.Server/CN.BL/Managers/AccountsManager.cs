using CN.Common.Contracts;
using CN.Common.Contracts.IManagers;
using CN.Common.Contracts.IRepositories;
using CN.Common.Enums;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.BL.Managers
{
    public class AccountsManager : IAccountsManager
    {
        public INetworkRepository networkRepository { get; set; }
        public AccountsManager(INetworkRepository networkRepository)
        {
            this.networkRepository = networkRepository;
        }


        public User UserLogin(UserLogin userLogin)
        {
            User user = networkRepository.GetUserByUsername(userLogin.Username);
            if (user != null)
            {
                if (user.Password == userLogin.Password)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public string UpdateExisitngClient(Client client)
        {
            //updates the details of an exisitng client
            string returnStr = "";
            if (networkRepository.IsLineNumberExists(client))
            {
                returnStr = "Line already in use";
            }
            else
            {
                networkRepository.UpdateClientDetails(client);
            }
            return returnStr;

        }

        public List<string> AddNewClient(Client client)
        {
            //adds a new client
            List<string> info = new List<string>();
            if (networkRepository.IsClientIdExisits(client.ID))
            {
                info.Add("Client ID already exisits");
            }
            if (networkRepository.IsLineNumberExists(client))
            {
                info.Add("Line already in use");
            }
            if (info.Count == 0)
            {
                networkRepository.AddNewClient(client);
            }

            return info;
        }

        public List<Client> GetAllClients()
        {
            //returns a list of the clients from the repository
            return networkRepository.GetAllClients();
        }

        public bool IsClientIdExists(string clientId)
        {
            //returns if the client id already exists
            return networkRepository.IsClientIdExisits(clientId);
        }

        public string DeleteClient(string id)
        {
            //deletes the client, his lines and his packages
            return networkRepository.DeleteClient(id);
        }

    }
}
