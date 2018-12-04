using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class Call
    {
        public Call()
        {

        }
        public Call(string LineID,double Duration,double ExternalPrice,string DestinationNumber)
        {
            this.LineID = LineID;
            this.Duration = Duration;
            this.ExternalPrice = ExternalPrice;
            this.DestinationNumber = DestinationNumber;
            this.DateOfCall = DateTime.Now;
        }
        public int ID { get; set; }
        public string LineID { get; set; }
        public double Duration { get; set; }
        public double ExternalPrice { get; set; }
        public string DestinationNumber { get; set; }
        public DateTime DateOfCall { get; set; }

    }
}
