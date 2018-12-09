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
        public ILinesManager linesManager { get; set; }
        public AccountsManager(INetworkRepository networkRepository, ILinesManager linesManager)
        {
            this.networkRepository = networkRepository;
            this.linesManager = linesManager;
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

        public List<Client> SearchForClients(string input)
        {
            //searches for the client that their fields matches this input
            input = input.ToLower();
            if (string.IsNullOrWhiteSpace(input))
            {
                return networkRepository.GetAllClients();
            }
            else
            {
                return networkRepository.GetAllClients().Where(c => c.FirstName.ToLower().Contains(input) || c.LastName.ToLower().Contains(input) || c.ID.Contains(input) || c.Address.ToLower().Contains(input)).ToList();
            }
        }

        public Client ClientLogin(ClientLogin clientLogin)
        {
            //tries a client login, returns null if info is unvalid
            Client client = networkRepository.GetClientByID(clientLogin.ID);
            if (client != null)
            {
                if (client.BirthDate.Year == clientLogin.YearOfBirth)
                {
                    return client;
                }
            }
            return null;
        }

        public List<Client> GetMostValueClients()
        {
            //returns the 10 most valueable clients
            List<Client> allClients = networkRepository.GetAllClients();
            foreach (var client in allClients)
            {
                client.Value = linesManager.GetClientValue(client);
            }
            return allClients.OrderByDescending(c => c.Value).Take(10).ToList();
        }

        public List<Client> GetMostCallingToCenter()
        {
            //gets the clients that called the most to the center
            return networkRepository.GetAllClients().OrderByDescending(c => c.CallsToCenter).Take(10).ToList();
        }

        public List<User> GetBestSellers()
        {
            //gets the sellers who sold the most lines
            List<User> users = networkRepository.GetAllUsers();
            foreach (var line in networkRepository.GetAllLines())
            {
                User user = users.FirstOrDefault(u => u.ID == line.EmployeeID);
                if (user != null)
                {
                    user.NumOfSales++;
                }
            }
            return users;
        }

        public RequestStatusEnum SendException(Error error)
        {
            //saves the exception to the database
            return networkRepository.SaveException(error);
        }

        public RequestStatusEnum SendExceptionsList(List<Error> errors)
        {
            //saves the exceptions to the database
            return networkRepository.SaveExceptionsList(errors);
        }
    }
}
