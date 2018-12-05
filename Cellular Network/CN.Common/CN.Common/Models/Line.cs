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
        public Line(string ClientID, string Number, LineStatusEnum Status, int PackageID,int EmployeeID)
        {
            this.ClientID = ClientID;
            this.Number = Number;
            this.Status = Status;
            this.PackageID = PackageID;
            this.EmployeeID = EmployeeID;
            DateAdded = DateTime.Now;
        }
        [Key]
        public string Number { get; set; }
        public string ClientID { get; set; }
        public LineStatusEnum Status { get; set; }
        public int PackageID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
