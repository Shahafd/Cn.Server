using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models
{
    public class PackageDetails
    {
        public PackageDetails()
        {

        }
        public PackageDetails(int PackageID, string PackageDescription, int MaxMinutes, double UsedMinutes, int MaxSMS, int UsedSMS, double FixedSmsPrice, double FixedCallPrice, double DiscountPercentage, int SelectedNumbersID, string MostCalledNumber)
        {
            this.PackageID = PackageID;
            this.PackageDescription = PackageDescription;
            this.MaxMinutes = MaxMinutes;
            this.UsedMinutes = UsedMinutes;
            this.MaxSMS = MaxSMS;
            this.UsedSMS = UsedSMS;
            this.FixedSmsPrice = FixedSmsPrice;
            this.FixedCallPrice = FixedCallPrice;
            this.DiscountPercentage = DiscountPercentage;
            this.SelectedNumbersID = SelectedNumbersID;
            this.MostCalledNumber = MostCalledNumber;
        }
        public int ID { get; set; }
        public int PackageID { get; set; }
        public string PackageDescription { get; set; }
        public int MaxMinutes { get; set; }
        public double UsedMinutes { get; set; }
        public int MaxSMS { get; set; }
        public int UsedSMS { get; set; }
        public double FixedSmsPrice { get; set; }
        public double FixedCallPrice { get; set; }
        public double DiscountPercentage { get; set; }
        public int SelectedNumbersID { get; set; }
        public string MostCalledNumber { get; set; }
    }
}
