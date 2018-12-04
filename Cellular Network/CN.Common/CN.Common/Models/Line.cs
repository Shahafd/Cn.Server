using CN.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class Line
    {
        public Line()
        {

        }
        public Line(string ClientID, string Number, LineStatusEnum Status, int PackageID)
        {
            this.ClientID = ClientID;
            this.Number = Number;
            this.Status = Status;
            this.PackageID = PackageID;
           
        }
        [Key]
        public string Number { get; set; }
        public string ClientID { get; set; }
        public LineStatusEnum Status { get; set; }
        public int PackageID { get; set; }
    }
}
