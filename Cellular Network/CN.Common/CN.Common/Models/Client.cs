
using CN.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class Client
    {
        public Client()
        {

        }
        public Client(string ID, string FirstName, string LastName, ClientTypeEnum ClientType, string Address, string ContactNumber, DateTime BirthDate)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.ClientType = ClientType;
            this.Address = Address;
            this.ContactNumber = ContactNumber;
            this.BirthDate = BirthDate;
        }
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public int CallsToCenter { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
