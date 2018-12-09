using CN.Common.Configs;
using CN.Common.Enums;
using CN.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.DAL.Databases
{
    class CnContextMockSeedInitializer : CreateDatabaseIfNotExists<CnContextMock>
    {
        protected override void Seed(CnContextMock context)
        {
            //USERS
            context.Users.Add(new User { Username = "Omer", Password = "123457", Type = UserTypeEnum.Admin });
            context.Users.Add(new User { Username = "Shahaf", Password = "123456", Type = UserTypeEnum.Admin });

            double BussinessCallPrice = PricesConfigs.CallMinPrice - (PricesConfigs.CallMinPrice * PricesConfigs.BussinessDiscountPrecentage / 100);

            double VipCallPrice = PricesConfigs.CallMinPrice - (PricesConfigs.CallMinPrice * PricesConfigs.VipDiscountPrecentage / 100);

            double BussinessSmsPrice = PricesConfigs.SmsPrice - (PricesConfigs.SmsPrice * PricesConfigs.BussinessDiscountPrecentage / 100);

            double VipSmsPrice = PricesConfigs.SmsPrice - (PricesConfigs.SmsPrice * PricesConfigs.VipDiscountPrecentage / 100);

            //CLIENTS TYPES
            context.ClientTypes.Add(new ClientType(1, "Private", PricesConfigs.CallMinPrice, PricesConfigs.SmsPrice));
            context.ClientTypes.Add(new ClientType(2, "VIP", VipCallPrice, VipSmsPrice));
            context.ClientTypes.Add(new ClientType(3, "Bussiness", BussinessCallPrice, BussinessSmsPrice));

            //PACKAGES
            context.Packages.Add(new Package("Soldier Package", 50, true));
            context.Packages.Add(new Package("Basic Package", 100, true));
            context.Packages.Add(new Package("Premium Package", 150, true));

            //PACKAGE DETAILS
            context.PackageDetails.Add(new PackageDetails(1, "A Package for soldiers", 200, 0, 200, 0, PricesConfigs.SmsPrice, PricesConfigs.CallMinPrice, 15, -1, null));
            context.PackageDetails.Add(new PackageDetails(2, "A basic Package", 200, 0, 200, 0, PricesConfigs.SmsPrice, PricesConfigs.CallMinPrice, 0, -1, null));
            context.PackageDetails.Add(new PackageDetails(3, "A premium Package", 350, 0, 350, 0, PricesConfigs.SmsPrice, PricesConfigs.CallMinPrice, 20, -1, null));

            //Clients
            string dateStr = "Jan 7, 1994";
            DateTime birthDate = DateTime.Parse(dateStr);
            context.Clients.Add(new Client("312149891", "Shahaf", "Dahan", ClientTypeEnum.Private, "Tishbi 17", "0523974471", birthDate));

            context.SaveChanges();
        }
    }
}
