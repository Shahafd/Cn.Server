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
    public class LinesManager : ILinesManager
    {
        INetworkRepository networkRepository { get; set; }
        public LinesManager(INetworkRepository networkRepository)
        {
            this.networkRepository = networkRepository;
        }
        public string GetNewLine(Client client)
        {
            //gets a new line,if first line: returns the clients contacts number,else generates a new 1
            List<Line> clientLines = networkRepository.GetClientLines(client.ID);
            if (clientLines.Count == 0)
            {
                return client.ContactNumber;
            }
            else return networkRepository.GetNewNumber();
        }

        public List<Package> GetDefaultPackages()
        {
            //returns the default packages
            return networkRepository.GetDefaultPackages();
        }

        public PackageDetails GetPackageDetailsByPackageId(int id)
        {
            //returns the package details for this package id
            return networkRepository.GetPackageDetailsByPackageId(id);
        }

        public SelectedNumbers GetSelectedNumbersById(int selectedNumbersId)
        {
            //returns the selected lines that matches this id
            return networkRepository.GetSelectedNumbersById(selectedNumbersId);
        }
        public List<Line> GetClientLinesByClientId(string id)
        {
            //returns a list of string represnting the client lines
            return networkRepository.GetClientLines(id);
        }

        public Package GetPackageByLineId(string lineId)
        {
            //returns the package that matches this line id
            return networkRepository.GetPackageByLineId(lineId);
        }

        public RequestStatusEnum SendLinePackageObj(LinePackObject linePackObj)
        {
            //check if the line needs to be created or updated and acts accordingly
            if (networkRepository.IsLineExists(linePackObj.LineNumber))
            {
                return networkRepository.UpdateLinePackage(linePackObj);
            }
            else
            {
                return networkRepository.CreateNewLinePackage(linePackObj);
            }
        }

        public RequestStatusEnum DeleteLine(string line)
        {
            //deletes the line and all its belongings
            return networkRepository.DeleteLine(line);
        }

        public LineStatusEnum GetLineStatus(string line)
        {
            //returns the status of the line
            return networkRepository.GetLineStatus(line);
        }

        public RequestStatusEnum UpdateLineStatus(Line line)
        {
            //updates the line status
            return networkRepository.UpdateLineStatus(line);
        }
        public ClientBill GetBillForSpecificLines(string clientId, YearAndMonth Date, List<string> lines)
        {
            //returns a bill for specific lines of this client
            List<Receipt> recepits = new List<Receipt>();
            Client client = networkRepository.GetClientByID(clientId);
            foreach (var item in lines)
            {
                recepits.Add(GetRecipetByLineAndDate(item, Date));
            }
            return new ClientBill($"{client.LastName} {client.FirstName}", recepits);
        }
        public ClientBill GetBillForClientByDate(string clientId, YearAndMonth Date)
        {
            //returns a bill for all the lines of this client
            List<Receipt> recepits = new List<Receipt>();
            Client client = networkRepository.GetClientByID(clientId);
            foreach (var item in networkRepository.GetClientLines(clientId))
            {
                recepits.Add(GetRecipetByLineAndDate(item.Number, Date));
            }
            return new ClientBill($"{client.LastName} {client.FirstName}", recepits);
        }
        public Receipt GetRecipetByLineAndDate(string lineNumber, YearAndMonth Date)
        {
            //gets a recepit for this month of the year
            Line line = networkRepository.GetLineById(lineNumber);
            Package package = networkRepository.GetPackageByLineId(line.Number);
            PackageDetails packDet = networkRepository.GetPackageDetailsByPackageId(package.ID);
            SelectedNumbers selectedNums = networkRepository.GetSelectedNumbersById(packDet.SelectedNumbersID);
            double MinutesToContacts = GetMinutesToContacts(lineNumber, Date);
            double SMSToContacts = GetSMSToContacts(lineNumber, Date);
            return new Receipt(line, package, packDet, MinutesToContacts, SMSToContacts);
        }
        public double GetPriceForPackageByLine(string lineNumber, Package package, YearAndMonth Date)
        {
            //gets the price for this line with this package
            Line line = networkRepository.GetLineById(lineNumber);
            Package packageForLine = networkRepository.GetPackageByLineId(line.Number);
            PackageDetails packDetForLine = networkRepository.GetPackageDetailsByPackageId(package.ID);
            SelectedNumbers selectedNums = networkRepository.GetSelectedNumbersById(packDetForLine.SelectedNumbersID);
            double MinutesToContacts = GetMinutesToContacts(lineNumber, Date);
            double SMSToContacts = GetSMSToContacts(lineNumber, Date);
            Receipt recepitForPackage = new Receipt(line, package, packDetForLine, MinutesToContacts, SMSToContacts);
            return recepitForPackage.TotalPayment;
        }
        private double GetSMSToContacts(string lineNumber, YearAndMonth date)
        {
            //returns the amount of mintues a client smsed his contacts
            List<SMS> smsToContacts = networkRepository.GetSMSToContactsByDate(lineNumber, date);
            return smsToContacts.Count;
        }

        private double GetMinutesToContacts(string lineNumber, YearAndMonth Date)
        {
            //return the amount of minutes a client called his contacts
            double sumOfMinutes = 0;
            List<Call> callsToContacts = networkRepository.GetCallsToContactsByDate(lineNumber, Date);
            foreach (var item in callsToContacts)
            {
                sumOfMinutes += item.Duration;
            }
            return sumOfMinutes;
        }
        public LineDetails GetLineDetails(string lineNumber)
        {
            //Gets the line details for the web client
            Client client = networkRepository.GetClientByNumber(lineNumber);
            YearAndMonth date = new YearAndMonth(DateTime.Now.Year, DateTime.Now.Month);
            Receipt mainRecepit = GetRecipetByLineAndDate(lineNumber, new YearAndMonth(DateTime.Now.Year, DateTime.Now.Month));
            List<Package> DefaultPackages = GetDefaultPackages();

            string Package1Name = DefaultPackages[0].PackageName;
            double Package1Price = GetPriceForPackageByLine(lineNumber, DefaultPackages[0], date);
            string Package2Name = DefaultPackages[1].PackageName;
            double Package2Price = GetPriceForPackageByLine(lineNumber, DefaultPackages[1], date);
            string Package3Name = DefaultPackages[2].PackageName;
            double Package3Price = GetPriceForPackageByLine(lineNumber, DefaultPackages[2], date);
            return new LineDetails(mainRecepit, GetClientValue(client), Package1Name, Package2Name, Package3Name, Package1Price, Package2Price, Package3Price);
        }

        private double GetClientValue(Client client)
        {
            //returns the calculation of the clients's value
            List<Line> clientLines = networkRepository.GetClientLines(client.ID);
            int numOfLines = clientLines.Count;
            if (numOfLines > 4)
            {
                numOfLines = 4;
            }
            double sumOfLast3Receppits= GetSumOfLast3Months(client)/1000;
            if (sumOfLast3Receppits > 6)
            {
                sumOfLast3Receppits = 6;
            }
            double callsToCenterVal = client.CallsToCenter * 0.1;
            if (callsToCenterVal > -3)
            {
                callsToCenterVal = -3;
            }
           return numOfLines + sumOfLast3Receppits + callsToCenterVal;
           
        }

        private double GetSumOfLast3Months(Client client)
        {
            //gets the sum the client paid for the last 3 month
            List<Receipt> allRecepits = new List<Receipt>();
            List<Line> clientLines = networkRepository.GetClientLines(client.ID);
            for (int i = 0; i < 3; i++)
            {
                YearAndMonth date = new YearAndMonth(DateTime.Now.Year - i, DateTime.Now.Month - i);
                foreach (var item in clientLines)
                {
                    allRecepits.Add(GetRecipetByLineAndDate(item.Number, date));
                }
            }
            double sumOfRecepits=0;
            foreach (var item in allRecepits)
            {
                sumOfRecepits += item.TotalPayment;
            }
            return sumOfRecepits;
        }
    }
}
