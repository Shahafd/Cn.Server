using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class SMS
    {
        public SMS()
        {

        }
        public SMS(int LineID,double ExternalPrice,string DestinationNumber)
        {
            this.LineID = LineID;
            this.ExternalPrice = ExternalPrice;
            this.DestintationNumber = DestintationNumber;
        }
        public int ID { get; set; }
        public int LineID { get; set; }
        public double ExternalPrice { get; set; }
        public string DestintationNumber { get; set; }
    }
}
