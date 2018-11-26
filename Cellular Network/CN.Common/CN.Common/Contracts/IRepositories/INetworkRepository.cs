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
        List<Client> GetAllClients();
        string DeleteClient(string id);
        bool IsLineNumberExists(Client client);
        string GetNewNumber();
        List<Package> GetDefaultPackages();
        PackageDetails GetPackageDetailsByPackageId(int id);
        SelectedNumbers GetSelectedNumbersById(int selectedNumbersId);

        List<Line> GetClientLines(string clientId);
        RequestStatusEnum AddCall(Call call);
        RequestStatusEnum AddSMS(SMS sms);
        Package GetPackageByLineId(string lineId);
    }
}
