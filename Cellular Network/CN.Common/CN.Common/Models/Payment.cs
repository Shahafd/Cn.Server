using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
   public class Payment
    {
        public Payment()
        {

        }
        public Payment(int ClientID,DateTime DateOfPayment,double TotalPayment)
        {
            this.ClientID = ClientID;
            this.DateOfPayment = DateOfPayment;
            this.TotalPayment = TotalPayment;
        }
        public int ID { get; set; }
        public int ClientID { get; set; }
        public DateTime DateOfPayment { get; set; }
        public double TotalPayment { get; set; }
    }
}
