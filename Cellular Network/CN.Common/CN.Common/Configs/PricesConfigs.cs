using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Configs
{
    public static class PricesConfigs
    {
        public const double SmsPrice = 0.40;// price of a sms
        public const double CallMinPrice = 0.80;//price of a call per minute

        public const double VipDiscountPrecentage = 15;
        public const double BussinessDiscountPrecentage = 10;

        public const int MaxMinutesInPackage = 1000;
        public const int MaxSMSInPackage = 2000;
        public const int MaxMinutePrice = 5;
        public const int MaxSMSPrice = 2;
        public const int MaxDiscountPrecentage = 40;

        public const int MinPackagePrice = 20;
        public const int MaxPackagePrice = 200;

    }
}
