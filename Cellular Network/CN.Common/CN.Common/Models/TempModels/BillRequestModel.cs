using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
   public class BillRequestModel
    {
        public string ClientID { get; set; }
        public List<string> Lines { get; set; }
        public YearAndMonth Date{ get; set; }
        public BillRequestModel(string ClientID,List<string>Lines,YearAndMonth Date)
        {
            this.ClientID = ClientID;
            this.Lines = Lines;
            this.Date = Date;
        }
    }
}
