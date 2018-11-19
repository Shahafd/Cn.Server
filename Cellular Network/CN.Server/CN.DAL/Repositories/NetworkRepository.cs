using CN.Common.Contracts.IRepositories;
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
    }
}
