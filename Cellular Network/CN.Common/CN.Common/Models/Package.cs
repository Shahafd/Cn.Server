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
        public Package(string PackageName,double PackageTotalPrice,bool DefaultPackage=false)
        {
            this.PackageName = PackageName;
            this.PackageTotalPrice = PackageTotalPrice;
            this.DefaultPackage = DefaultPackage;
        }
        public int ID { get; set; }
        public string PackageName { get; set; }
        public double PackageTotalPrice { get; set; }
        public bool DefaultPackage { get; set; }
    }
}
