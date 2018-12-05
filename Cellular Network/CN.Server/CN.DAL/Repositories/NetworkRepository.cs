using CN.Common.Contracts.IRepositories;
using CN.Common.Enums;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.DAL.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.DAL.Repositories
{
    public class NetworkRepository : INetworkRepository
    {
        public List<Client> Clients { get; set; }
        public List<User> Users { get; set; }
        public List<ClientType> ClientTypes { get; set; }
        public List<Call> Calls { get; set; }
        public List<Line> Lines { get; set; }
        public List<Package> Packages { get; set; }
        public List<PackageDetails> PackageDetails { get; set; }
        public List<SelectedNumbers> SelectedNumbers { get; set; }
        public List<SMS> SMS { get; set; }
        public List<Payment> Payments { get; set; }
        public static readonly object callLock = new object();
        public static readonly object dbLock = new object();
        public NetworkRepository()
        {

            InitCollections();
            LoadCollections();
        }
        private void LoadLinesAndPackges()
        {
            //loads line,packages and etc
            using (CnContext context = new CnContext())
            {
                Lines = context.Lines.ToList();
                Packages = context.Packages.ToList();
                PackageDetails = context.PackageDetails.ToList();
                SelectedNumbers = context.SelectedNumbers.ToList();
            }
        }
        private void LoadClients()
        {
            //loads clients,users and etc
            using (CnContext context = new CnContext())
            {
                Clients = context.Clients.ToList();
                Users = context.Users.ToList();
                ClientTypes = context.ClientTypes.ToList();
            }
        }
        private void LoadCallsAndSms()
        {
            //loads calls,sms and payments
            using (CnContext context = new CnContext())
            {
                Calls = context.Calls.ToList();
                SMS = context.SMS.ToList();
                Payments = context.Payments.ToList();
            }
        }
        private void LoadCollections()
        {
            //Loads the collections from the database
            LoadLinesAndPackges();
            LoadClients();
            LoadCallsAndSms();
        }
        private void InitCollections()
        {
            //Inits the collections
            Clients = new List<Client>();
            Users = new List<User>();
            ClientTypes = new List<ClientType>();
            Calls = new List<Call>();
            Lines = new List<Line>();
            Packages = new List<Package>();
            PackageDetails = new List<PackageDetails>();
            SelectedNumbers = new List<SelectedNumbers>();
            SMS = new List<SMS>();
            Payments = new List<Payment>();

        }

        public bool IsUserNameExists(string userName)
        {
            //returns true if the user name already exists
            return Users.Exists(u => u.Username.ToLower() == userName.ToLower());
        }

        public User GetUserByUsername(string username)
        {
            //returns the user that matches this username
            return Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
        }

        public Client GetClientByID(string iD)
        {
            //returns the client that matches this id
            return Clients.FirstOrDefault(c => c.ID == iD);
        }

        public RequestStatusEnum UpdateClientDetails(Client client)
        {
            //updates the details of an exisiting client

            using (CnContext context = new CnContext())
            {
                Client dbClient = context.Clients.FirstOrDefault(c => c.ID == client.ID);
                dbClient.FirstName = client.FirstName;
                dbClient.LastName = client.LastName;
                dbClient.Address = client.Address;
                dbClient.BirthDate = client.BirthDate;
                dbClient.ClientType = client.ClientType;
                dbClient.ContactNumber = client.ContactNumber;
                context.SaveChanges();
            }
            Client toUpdate = GetClientByID(client.ID);
            toUpdate.FirstName = client.FirstName;
            toUpdate.LastName = client.LastName;
            toUpdate.Address = client.Address;
            toUpdate.BirthDate = client.BirthDate;
            toUpdate.ClientType = client.ClientType;
            toUpdate.ContactNumber = client.ContactNumber;

            return RequestStatusEnum.Success;
        }

        public RequestStatusEnum AddNewClient(Client client)
        {
            //adds a new Client

            using (CnContext context = new CnContext())
            {
                context.Clients.Add(client);
                context.SaveChanges();
            }
            Clients.Add(client);

            return RequestStatusEnum.Success;
        }

        public bool IsClientIdExisits(string iD)
        {
            //checks if the Id already exisits
            return Clients.Exists(c => c.ID == iD);
        }

        public List<Client> GetAllClients()
        {
            //returns all the clients
            return Clients;
        }

        public string DeleteClient(string id)
        {
            //moves the client to the un active clients 
            Client client = GetClientByID(id);
            DiactivateClientLinesById(id);
            using (CnContext context = new CnContext())
            {
                UnActiveClient unActive = ConvertClientToUnActive(client);
                context.UnActiveClients.Add(unActive);
                //  context.Clients.Remove(c=)
                context.SaveChanges();
            }
            Clients.Remove(client);
            return "Client removed succesfully!";
        }

        private UnActiveClient ConvertClientToUnActive(Client client)
        {
            //converts a client model to an unactive model
            return new UnActiveClient { ID = client.ID, FirstName = client.FirstName, LastName = client.LastName, Address = client.Address, BirthDate = client.BirthDate, CallsToCenter = client.CallsToCenter, ClientType = client.ClientType, ContactNumber = client.ContactNumber };
        }

        private void DiactivateClientLinesById(string id)
        {
            //deactivates the client lines
            List<Line> clientLines = GetClientLinesById(id);
            using (CnContext context = new CnContext())
            {
                foreach (var item in clientLines)
                {
                    Line dbLine = context.Lines.FirstOrDefault(l => l.ClientID == id);
                    if (dbLine != null)
                    {
                        dbLine.Status = LineStatusEnum.Blocked;
                    }
                    item.Status = LineStatusEnum.Blocked;
                }
                context.SaveChanges();
            }
        }

        private List<Line> GetClientLinesById(string id)
        {
            //gets all lines for this client
            return Lines.Where(l => l.ClientID == id).ToList();
        }

        public bool IsLineNumberExists(Client client)
        {
            //checks if the line number already used by another client
            Line line = Lines.FirstOrDefault(l => l.Number == client.ContactNumber);
            Client clientQ = Clients.FirstOrDefault(c => c.ContactNumber == client.ContactNumber && c.ID != client.ID);
            if (line == null || line != null && line.ClientID == client.ID)
            {
                if (clientQ == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        public string GetNewNumber()
        {
            //returns a new, unused umber
            Random rand = new Random();
            int randNum = rand.Next(100000000, 999999999);
            string randNumStr = "0" + randNum.ToString();
            if (IsNumberFree(randNumStr))
            {
                return randNumStr;
            }
            else
            {
                return GetNewNumber();
            }
        }

        private bool IsNumberFree(string randNumStr)
        {
            //checks if there is a line that already uses this number
            return Lines.Where(l => l.Number == randNumStr).Count() == 0;
        }

        public List<Package> GetDefaultPackages()
        {
            //returns the default packages
            return Packages.Where(p => p.DefaultPackage).ToList();
        }

        public PackageDetails GetPackageDetailsByPackageId(int id)
        {
            //returns the package details for this package id
            return PackageDetails.FirstOrDefault(pd => pd.PackageID == id);
        }

        public SelectedNumbers GetSelectedNumbersById(int selectedNumbersId)
        {
            //returns the selected numbers that matches this idd
            return SelectedNumbers.FirstOrDefault(sn => sn.ID == selectedNumbersId);
        }
        public async Task<bool> AddCall(Call call)
        {
            using (CnContext context = new CnContext())
            {
                context.Calls.Add(call);
                Package package = GetPackageByLineId(call.LineID);
                PackageDetails packDet = GetPackageDetailsByPackageId(package.ID);
                packDet.UsedMinutes += call.Duration;
                UpdateDBPackageDetails(packDet);
                context.SaveChanges();
            }
            Calls.Add(call);

            return true;

        }

        public async Task<bool> AddSms(SMS sms)
        {
            using (CnContext context = new CnContext())
            {
                context.SMS.Add(sms);
                Package package = GetPackageByLineId(sms.LineID);
                PackageDetails packDet = GetPackageDetailsByPackageId(package.ID);
                packDet.UsedSMS++;
                UpdateDBPackageDetails(packDet);
                context.SaveChanges();
            }
            SMS.Add(sms);
            return true;
        }

        private void UpdateDBPackageDetails(PackageDetails packDet)
        {
           //updates the package details in the database
           using(CnContext context= new CnContext())
            {
                PackageDetails packDetFromDb = context.PackageDetails.FirstOrDefault(pd => pd.ID == packDet.ID);
                if (packDetFromDb != null)
                {
                    packDetFromDb.UsedMinutes = packDet.UsedMinutes;
                    packDetFromDb.UsedSMS = packDet.UsedSMS;
                }
                context.SaveChanges();
            }
        }

        public List<Line> GetClientLines(string clientId)
        {
            //returns client lines
            return Lines.Where(l => l.ClientID == clientId).ToList();
        }

        public Package GetPackageByLineId(string lineId)
        {
            //returns the package that matches this line id
            Line line = GetLineById(lineId);
            if (line != null)
            {
                return Packages.FirstOrDefault(p => p.ID == line.PackageID);
            }
            else
            {
                return null;
            }
        }

        public Line GetLineById(string lineId)
        {
            //returns the line that matches this id
            return Lines.FirstOrDefault(l => l.Number == lineId);
        }

        public bool IsLineExists(string lineNumber)
        {
            //checks if the line already exists
            return Lines.Exists(l => l.Number == lineNumber);
        }

        public RequestStatusEnum UpdateLinePackage(LinePackObject linePackObj)
        {
            //updates the line,package,package details,and selected numbers
            using (CnContext context = new CnContext())
            {
                Package packpageFromDb = context.Packages.FirstOrDefault(p => p.ID == linePackObj.Package.ID);
                packpageFromDb.PackageTotalPrice = linePackObj.Package.PackageTotalPrice;
                PackageDetails packdetFromDb = context.PackageDetails.FirstOrDefault(pd => pd.ID == linePackObj.PackageDetails.ID);
                packdetFromDb.DiscountPercentage = linePackObj.PackageDetails.DiscountPercentage;
                packdetFromDb.FixedCallPrice = linePackObj.PackageDetails.FixedCallPrice;
                packdetFromDb.FixedSmsPrice = linePackObj.PackageDetails.FixedSmsPrice;
                packdetFromDb.MaxMinutes = linePackObj.PackageDetails.MaxMinutes;
                packdetFromDb.MaxSMS = linePackObj.PackageDetails.MaxSMS;
                packdetFromDb.MostCalledNumber = linePackObj.PackageDetails.MostCalledNumber;
                SelectedNumbers selectedNumsFromDb = context.SelectedNumbers.FirstOrDefault(sn => sn.ID == linePackObj.SelectedNumbers.ID);
                selectedNumsFromDb.FirstNumber = linePackObj.SelectedNumbers.FirstNumber;
                selectedNumsFromDb.SecondNumber = linePackObj.SelectedNumbers.SecondNumber;
                selectedNumsFromDb.ThirdNumber = linePackObj.SelectedNumbers.ThirdNumber;
                context.SaveChanges();
            }
            LoadCollections();
            return RequestStatusEnum.Success;
        }

        public RequestStatusEnum CreateNewLinePackage(LinePackObject linePackObj)
        {
            //creates a new line,package,package details,and selected numbers
            lock (dbLock)
            {
                using (CnContext context = new CnContext())
                {
                    context.Packages.Add(new Package(linePackObj.ClientId, linePackObj.Package.PackageTotalPrice));
                    context.SaveChanges();
                    LoadLinesAndPackges();

                    int PackageId = Packages.Last().ID;

                    context.SelectedNumbers.Add(new SelectedNumbers(linePackObj.SelectedNumbers.FirstNumber, linePackObj.SelectedNumbers.SecondNumber, linePackObj.SelectedNumbers.ThirdNumber));
                    context.SaveChanges();
                    LoadLinesAndPackges();
                    int selectedNumId = SelectedNumbers.Last().ID;

                    context.PackageDetails.Add(new PackageDetails(PackageId, "Custom Package", linePackObj.PackageDetails.MaxMinutes, 0, linePackObj.PackageDetails.MaxSMS, 0, linePackObj.PackageDetails.FixedSmsPrice, linePackObj.PackageDetails.FixedCallPrice, linePackObj.PackageDetails.DiscountPercentage, selectedNumId, linePackObj.PackageDetails.MostCalledNumber));

                    context.Lines.Add(new Line(linePackObj.ClientId, linePackObj.LineNumber, LineStatusEnum.Available, PackageId,linePackObj.EmployeeID));
                    context.SaveChanges();
                }
                LoadCollections();
                return RequestStatusEnum.Success;
            }
        }

        public RequestStatusEnum DeleteLine(string line)
        {
            //deletes the line and all its belongings
            lock (dbLock)
            {
                using (CnContext context = new CnContext())
                {
                    Line lineFromDb = context.Lines.FirstOrDefault(l => l.Number == line);
                    Package packageFromDb = context.Packages.FirstOrDefault(p => p.ID == lineFromDb.PackageID);
                    PackageDetails packDetFromDb = context.PackageDetails.FirstOrDefault(pd => pd.PackageID == packageFromDb.ID);
                    SelectedNumbers selectedNumsFromDb = context.SelectedNumbers.FirstOrDefault(sn => sn.ID == packDetFromDb.SelectedNumbersID);
                    context.Lines.Remove(lineFromDb);
                    context.Packages.Remove(packageFromDb);
                    context.PackageDetails.Remove(packDetFromDb);
                    context.SelectedNumbers.Remove(selectedNumsFromDb);
                    context.SaveChanges();
                    LoadLinesAndPackges();
                }
                return RequestStatusEnum.Success;
            }
        }

        public LineStatusEnum GetLineStatus(string line)
        {
            //returns the status of this line
            return GetLineById(line).Status;
        }

        public RequestStatusEnum UpdateLineStatus(Line line)
        {
            //updates the line's new status
            using (CnContext context = new CnContext())
            {
                Line lineFromDb = context.Lines.FirstOrDefault(l => l.Number == line.Number);
                if (lineFromDb != null)
                {
                    lineFromDb.Status = line.Status;
                    context.SaveChanges();
                    LoadLinesAndPackges();
                    return RequestStatusEnum.Success;
                }
                else
                {
                    return RequestStatusEnum.Error;
                }
            }
        }



        public List<Call> GetCallsToContactsByDate(string lineNumber, YearAndMonth date)
        {
            //returns all the calls that a client made to his contacts in this month of the year
            Package package = GetPackageByLineId(lineNumber);
            PackageDetails packDet = GetPackageDetailsByPackageId(package.ID);
            SelectedNumbers selectedNums = GetSelectedNumbersById(packDet.SelectedNumbersID);
            return Calls.Where(c => c.LineID == lineNumber && c.DestinationNumber == selectedNums.FirstNumber || c.DestinationNumber == selectedNums.SecondNumber || c.DestinationNumber == selectedNums.ThirdNumber || c.DestinationNumber == packDet.MostCalledNumber && c.DateOfCall.Month == date.Month && c.DateOfCall.Year == date.Year).ToList();
        }

        public List<Call> GetCallsNotToContactsByDate(string lineNumber, YearAndMonth date)
        {
            //returns all the calls that a client made to clients other than his contacts in this month of the year
            Package package = GetPackageByLineId(lineNumber);
            PackageDetails packDet = GetPackageDetailsByPackageId(package.ID);
            SelectedNumbers selectedNums = GetSelectedNumbersById(packDet.SelectedNumbersID);
            return Calls.Where(c => c.LineID == lineNumber && c.DestinationNumber != selectedNums.FirstNumber && c.DestinationNumber != selectedNums.SecondNumber && c.DestinationNumber != selectedNums.ThirdNumber && c.DestinationNumber != packDet.MostCalledNumber && c.DateOfCall.Month == date.Month && c.DateOfCall.Year == date.Year).ToList();
        }

        public List<SMS> GetSMSToContactsByDate(string lineNumber, YearAndMonth date)
        {
            //returns all the sms that a client made to his contacts in this month of the year
            Package package = GetPackageByLineId(lineNumber);
            PackageDetails packDet = GetPackageDetailsByPackageId(package.ID);
            SelectedNumbers selectedNums = GetSelectedNumbersById(packDet.SelectedNumbersID);
            return SMS.Where(c => c.LineID == lineNumber && c.DestintationNumber == selectedNums.FirstNumber || c.DestintationNumber == selectedNums.SecondNumber || c.DestintationNumber == selectedNums.ThirdNumber || c.DestintationNumber == packDet.MostCalledNumber && c.DateOfSMS.Month == date.Month && c.DateOfSMS.Year == date.Year).ToList();
        }

        public Client GetClientByNumber(string lineNumber)
        {
            //returns the client that matches this number
            string clientId = Lines.FirstOrDefault(l => l.Number == lineNumber).ClientID;
            return GetClientByID(clientId);
        }
    }
}
