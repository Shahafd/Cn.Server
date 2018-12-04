using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class ClientLogin
    {
        public string ID { get; set; }
        public int YearOfBirth { get; set; }
        public ClientLogin()
        {

        }
        public ClientLogin(string ID,int YearOfBirth)
        {
            this.ID=ID;
            this.YearOfBirth = YearOfBirth;
        }
    }
}
