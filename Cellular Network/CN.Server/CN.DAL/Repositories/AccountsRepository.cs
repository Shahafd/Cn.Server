using CN.Common.Contracts;
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
    public class AccountsRepository : IAccountsRepository
    {
        public List<Client> Clients { get; set; }
        public List<User> Users { get; set; }
        public AccountsRepository()
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
            }
        }
        private void InitCollections()
        {
            //Inits the collections
            Clients = new List<Client>();
            Users = new List<User>();
        }

        public bool IsUserNameExists(string userName)
        {
            //returns true if the user name already exists
            return Users.Exists(u => u.Username.ToLower() == userName.ToLower());
        }

        public User TryLogin(UserLogin userLogin)
        {
            //returns the user if the fields are valid,returns null if not
            User user = Users.FirstOrDefault(u => u.Username.ToLower() == userLogin.Username.ToLower());
            Console.WriteLine(user.Username);
            if (user != null)
            {
                if (user.Password == userLogin.Password)
                {
                    return user;
                }
            }
            return null;


        }
    }
}
