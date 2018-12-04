using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class LineDetails
    {
        public double ClientValue { get; set; }
        public double Payment { get; set; }
        public double TotalMinutes { get; set; }//
        public double TotalSMS { get; set; }//
        public double TotalMinutesToContacts { get; set; }//
        public double TotalSMSToContacts { get; set; }//
        public string Package1Name { get; set; }
        public string Package2Name { get; set; }
        public string Package3Name { get; set; }
        public double Package1Price { get; set; }
        public double Package2Price { get; set; }
        public double Package3Price { get; set; }

        public LineDetails(Receipt recepit,double ClientValue, string Package1Name, string Package2Name, string Package3Name, double Package1Price, double Package2Price, double Package3Price)
        {
            this.ClientValue = ClientValue;
            TotalMinutes = recepit.UsedMinutes;
            TotalSMS = recepit.UsedSMS;
            TotalMinutesToContacts = recepit.MinutesToContacts;
            TotalSMSToContacts = recepit.SMSToContacts;
            this.Payment = recepit.TotalPayment;
            this.Package1Name = Package1Name;
            this.Package2Name = Package2Name;
            this.Package3Name = Package3Name;
            this.Package1Price = Package1Price;
            this.Package2Price = Package2Price;
            this.Package3Price = Package3Price;
        }
        public LineDetails()
        {

        }
    }
}
