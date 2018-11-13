using CN.Common.Configs;
using CN.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.DAL.Databases
{
    class CnContextSeedInitializer : CreateDatabaseIfNotExists<CnContext>
    {
        protected override void Seed(CnContext context)
        {
            context.Users.Add(new User { Username = "Omer", Password = "123456" });
            context.Users.Add(new User { Username = "Shahaf", Password = "123456" });

            double BussinessCallPrice = PricesConfigs.CallMinPrice - (PricesConfigs.CallMinPrice * PricesConfigs.BussinessDiscountPrecentage / 100);
            double VipCallPrice = PricesConfigs.CallMinPrice - (PricesConfigs.CallMinPrice * PricesConfigs.VipDiscountPrecentage / 100);
            double BussinessSmsPrice = PricesConfigs.SmsPrice - (PricesConfigs.SmsPrice * PricesConfigs.BussinessDiscountPrecentage / 100);
            double VipSmsPrice = PricesConfigs.SmsPrice - (PricesConfigs.SmsPrice * PricesConfigs.VipDiscountPrecentage / 100);
            context.ClientTypes.Add(new ClientType(1, "Private", PricesConfigs.CallMinPrice, PricesConfigs.SmsPrice));
            context.ClientTypes.Add(new ClientType(2, "VIP", VipCallPrice, VipSmsPrice));
            context.ClientTypes.Add(new ClientType(3, "Bussiness", BussinessCallPrice, BussinessSmsPrice));

            context.SaveChanges();
        }
    }
}
