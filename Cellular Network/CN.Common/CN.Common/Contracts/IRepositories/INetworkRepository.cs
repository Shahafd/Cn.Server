using CN.Common.Enums;
using CN.Common.Models;
using CN.Common.Models.TempModels;
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
        Package GetPackageByLineId(string lineId);
        bool IsLineExists(string lineNumber);
        RequestStatusEnum UpdateLinePackage(LinePackObject linePackObj);
        RequestStatusEnum CreateNewLinePackage(LinePackObject linePackObj);
        RequestStatusEnum DeleteLine(string line);
        LineStatusEnum GetLineStatus(string line);
        RequestStatusEnum UpdateLineStatus(Line line);
        Task<bool> AddCall(Call call);
        Task<bool> AddSms(SMS sms);
        Line GetLineById(string lineId);
        void AddCallsToCenter(string clientId);
        List<Call> GetCallsToContactsByDate(string lineNumber, YearAndMonth date);
        List<Call> GetCallsNotToContactsByDate(string lineNumber, YearAndMonth date);
        List<SMS> GetSMSToContactsByDate(string lineNumber, YearAndMonth date);
        Client GetClientByNumber(string lineNumber);
        List<User> GetAllUsers();
        List<Line> GetAllLines();
        RequestStatusEnum SaveException(Error error);
        RequestStatusEnum SaveExceptionsList(List<Error> errors);
        List<Call> GetCallsThisMonth(string lineNumber, YearAndMonth date);
        List<SMS> GetSMSThisMonth(string lineNumber, YearAndMonth date);
    }
}
