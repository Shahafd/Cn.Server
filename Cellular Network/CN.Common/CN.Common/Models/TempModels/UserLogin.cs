using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserLogin()
        {

        }
        public UserLogin(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
      


    }
}
