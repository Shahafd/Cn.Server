using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class LinePackObject
    {
        public string ClientId { get; set; }
        public string LineNumber { get; set; }
        public Package Package { get; set; }
        public PackageDetails PackageDetails{ get; set; }
        public SelectedNumbers SelectedNumbers { get; set; }
        public LinePackObject(string LineNumber,Package Package,PackageDetails PackageDetails,SelectedNumbers SelectedNumbers,string ClientId)
        {
            this.LineNumber = LineNumber;
            this.Package = Package;
            this.PackageDetails = PackageDetails;
            this.SelectedNumbers = SelectedNumbers;
            this.ClientId = ClientId;
        }
    }
}
