using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class ClientBill
    {
        public List<Receipt> Recepits { get; set; }
        public string ClientName { get; set; }
        public double TotalBill{ get; set; }
        public ClientBill(string ClientName,List<Receipt> Recepits )
        {
            TotalBill = 0;
            this.ClientName = ClientName;
            this.Recepits = Recepits;
            foreach (var item in Recepits)
            {
                this.Recepits.Add(item);
                TotalBill += item.TotalPayment;
            }
        }
        
    }
}
