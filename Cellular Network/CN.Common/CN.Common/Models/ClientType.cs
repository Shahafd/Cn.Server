using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class ClientType
    {
        public ClientType()
        {

        }
        public ClientType(int ID,string TypeName,double MinutesPrice,double SmsPrice)
        {
            this.ID = ID;
            this.TypeName = TypeName;
            this.MinutesPrice = MinutesPrice;
            this.SmsPrice = SmsPrice;
        }
       public int ID { get; set; } //1: Private, 2:Business, 3:VIP
       public string TypeName { get; set; }
       public double MinutesPrice { get; set; }
       public double SmsPrice { get; set; }
    }
}
