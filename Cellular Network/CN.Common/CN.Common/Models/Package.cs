using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class Package
    {
        public Package()
        {

        }
        public Package(string PackageName,string PackageTotalPrice)
        {
            this.PackageName = PackageName;
            this.PackageTotalPrice = PackageTotalPrice;
        }
        public int ID { get; set; }
        public string PackageName { get; set; }
        public string PackageTotalPrice { get; set; }
    }
}
