using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class Receipt
    {
        public int ReceiptID { get; set; }
        public string LineNumber { get; set; }//

        public double UsedMinutes { get; set; }//
        public double MinutesLeftInPackage { get; set; }//
        public double UsedSMS { get; set; }//
        public double SMSLeftInPackage { get; set; }//
        public double SMSUsagePrecentage { get; set; }//
        public double MinutesUsagePrecentage { get; set; }//
        public double PackagePrice { get; set; }//

        public double MinutesBeyondPackage { get; set; }//
        public double SMSBeyondPackage { get; set; }//
        public double PricePerMinute { get; set; }//
        public double PricePerSMS { get; set; }//
        public double MinutesToContacts { get; set; }//
        public double SMSToContacts { get; set; }//
        public double TotalPaidMinutes { get; set; }//
        public double TotalPaidSMS { get; set; }//

        public double DiscountToContactsPrecentage { get; set; }//

        public double TotalPayment { get; set; }

        public Receipt(Line line, Package package, PackageDetails packDet, double MinutesToContacts, double SMSToContacts)
        {
            LineNumber = line.Number;
            UsedMinutes = packDet.UsedMinutes;
            UsedSMS = packDet.UsedSMS;
            MinutesLeftInPackage = GetUnitsLeft(packDet.UsedSMS, packDet.MaxMinutes);
            SMSLeftInPackage = GetUnitsLeft(packDet.UsedMinutes, packDet.MaxSMS);
            MinutesUsagePrecentage = GetPrecentage(packDet.UsedMinutes, packDet.MaxMinutes);
            SMSUsagePrecentage = GetPrecentage(packDet.UsedSMS, packDet.MaxSMS);
            PackagePrice = package.PackageTotalPrice;
            this.MinutesToContacts = MinutesToContacts;
            this.SMSToContacts = SMSToContacts;

            MinutesBeyondPackage = CalcBeyondPackage(packDet.UsedMinutes, packDet.MaxMinutes);
            SMSBeyondPackage = CalcBeyondPackage(packDet.UsedSMS, packDet.MaxSMS);
            PricePerMinute = packDet.FixedCallPrice;
            PricePerSMS = packDet.FixedSmsPrice;
            DiscountToContactsPrecentage = packDet.DiscountPercentage;
            TotalPaidMinutes = CalcExtra(MinutesBeyondPackage, MinutesToContacts, PricePerMinute, DiscountToContactsPrecentage);
            TotalPaidSMS = CalcExtra(SMSBeyondPackage, SMSToContacts, PricePerSMS, DiscountToContactsPrecentage);
            TotalPayment = PackagePrice + TotalPaidMinutes + TotalPaidSMS;
        }

        private double CalcExtra(double beyondPackage, double toContacts, double pricePerUnit, double discountPrecentage)
        {
            if (beyondPackage == 0)
            {
                return 0;
            }
            else
            {
                //if the client made more calls to his contacts than the amount he exceeded, he will get the discount for all his beyond packages
                double extraToContacts;
                double extraNotToContacts;
                if (toContacts >= beyondPackage)
                {
                    extraToContacts = beyondPackage;
                }
                else
                {
                    extraToContacts = toContacts;
                }
                extraNotToContacts = beyondPackage - extraToContacts;
                double SumNotToContacts = extraNotToContacts * pricePerUnit;
                double SumToContacts = extraToContacts * pricePerUnit;
                double discountAmount = SumToContacts * discountPrecentage / 100;
                return SumNotToContacts + SumToContacts - discountAmount;
            }
        }

        private double CalcBeyondPackage(double usedMinutes, int maxMinutes)
        {
            if (usedMinutes > maxMinutes)
            {
                return usedMinutes - maxMinutes;
            }
            else
            {
                return 0;
            }
        }

        private double GetUnitsLeft(double used, int max)
        {
            if (used >= max)
            {
                return 0;
            }
            else
            {
                return max - used;
            }
        }
        private double GetPrecentage(double used, int max)
        {
            if (used >= max)
            {
                return 100;
            }
            else
            {
                return used / max * 100;
            }
        }

    }
}
