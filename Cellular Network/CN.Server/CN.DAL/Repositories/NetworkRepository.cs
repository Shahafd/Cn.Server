using CN.Common.Contracts.IRepositories;
using CN.Common.Enums;
using CN.Common.Models;
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
        public NetworkRepository()
        {

            InitCollections();
            LoadCollections();
        }

        private void LoadCollections()
        {
            //Loads the collections from the database
            using (CnContext context = new CnContext())
            {
                Clients = context.Clients.ToList();
                Users = context.Users.ToList();
                ClientTypes = context.ClientTypes.ToList();
                Calls = context.Calls.ToList();
                Lines = context.Lines.ToList();
                Packages = context.Packages.ToList();
                PackageDetails = context.PackageDetails.ToList();
                SelectedNumbers = context.SelectedNumbers.ToList();
                SMS = context.SMS.ToList();
                Payments = context.Payments.ToList();
            }
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
        public RequestStatusEnum AddCall(Call call)
        {
            using (CnContext context = new CnContext())
            {
                context.Calls.Add(call);
                context.SaveChanges();
            }
            Calls.Add(call);

            return RequestStatusEnum.Success;

        }

        public RequestStatusEnum AddSMS(SMS sms)
        {
            throw new NotImplementedException();
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
            return Packages.FirstOrDefault(p => p.ID == line.PackageID);
        }

        private Line GetLineById(string lineId)
        {
            //returns the line that matches this id
            return Lines.FirstOrDefault(l => l.Number == lineId);
        }
    }
}
