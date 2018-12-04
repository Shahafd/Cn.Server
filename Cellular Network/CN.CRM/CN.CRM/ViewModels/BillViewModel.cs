using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CN.CRM.ViewModels
{
    class BillViewModel : IBillViewModel
    {
        ILogger logger { get; set; }
        //IInputsValidator inputsValidator { get; set; }
        IHttpClient httpClient { get; set; }
        public string ClientNameStr { get; set; }
        public string MonthStr { get; set; }
        public string PriceStr { get; set; }


        Grid DynamicGrid { get; set; }
        ClientBill clientBill = new ClientBill("omer", new List<Receipt> {new Receipt(new Line("12345678","222222",Common.Enums.LineStatusEnum.Available,1),new Package("poor",200.0,false),new PackageDetails(1,"poor",300,30.0,600,400,40.0,20.0,30.0,2,"3232322232"),50.0,40.0),
                                                                         new Receipt(new Line("87654321","555555",Common.Enums.LineStatusEnum.Available,1),new Package("poor",100.0,false),new PackageDetails(1,"poor",300,30.0,600,400,40.0,20.0,30.0,2,"3232322232"),50.0,40.0)  });


        public BillViewModel(ILogger logger, IHttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.ClientNameStr = clientBill.ClientName;
            this.MonthStr = "TODO MONTH";
            this.PriceStr = clientBill.TotalBill.ToString();


        }

        public void GetGridFromWindow(Grid grid)
        {
            DynamicGrid = grid;
            DynamicGrid.Width = 800;
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;
            DynamicGrid.Background = new SolidColorBrush(Colors.Gray);
            //DynamicGrid.ShowGridLines = true;
            for (int i = 0; i < clientBill.Recepits.Count; i++)
            {

                RowDefinition gridRowD = new RowDefinition();
                DynamicGrid.RowDefinitions.Add(gridRowD);
                Grid LineGrid = CreateGridForLine(clientBill.Recepits[i]);
                DynamicGrid.Children.Add(LineGrid);
                Grid.SetRow(LineGrid, i);
            }


        }

        private Grid CreateGridForLine(Receipt receipt)
        {
            Grid GridL = new Grid();
            RowDefinition gridRow0 = new RowDefinition();
            RowDefinition gridRow1 = new RowDefinition();
            RowDefinition gridRow2 = new RowDefinition();
            RowDefinition gridRow3 = new RowDefinition();
            RowDefinition gridRow4 = new RowDefinition();
            RowDefinition gridRow5 = new RowDefinition();
            RowDefinition gridRow6 = new RowDefinition();
            RowDefinition gridRow7 = new RowDefinition();
            RowDefinition gridRow8 = new RowDefinition();
            RowDefinition gridRow9 = new RowDefinition();
            RowDefinition gridRow10 = new RowDefinition();
            RowDefinition gridRow11 = new RowDefinition();
            RowDefinition gridRow12 = new RowDefinition();
            GridL.RowDefinitions.Add(gridRow0);
            GridL.RowDefinitions.Add(gridRow1);
            GridL.RowDefinitions.Add(gridRow2);
            GridL.RowDefinitions.Add(gridRow3);
            GridL.RowDefinitions.Add(gridRow4);
            GridL.RowDefinitions.Add(gridRow5);
            GridL.RowDefinitions.Add(gridRow6);
            GridL.RowDefinitions.Add(gridRow7);
            GridL.RowDefinitions.Add(gridRow8);
            GridL.RowDefinitions.Add(gridRow9);
            GridL.RowDefinitions.Add(gridRow10);
            GridL.RowDefinitions.Add(gridRow11);
            GridL.RowDefinitions.Add(gridRow12);
            ColumnDefinition gridCol0 = new ColumnDefinition();
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            GridL.ColumnDefinitions.Add(gridCol0);
            GridL.ColumnDefinitions.Add(gridCol1);
            GridL.ColumnDefinitions.Add(gridCol2);


            //Line number: "21312312312"
            TextBlock LineNumTxt = new TextBlock();
            LineNumTxt.Text = "Line Number:" + receipt.LineNumber;
            LineNumTxt.FontSize = 16;
            LineNumTxt.HorizontalAlignment = HorizontalAlignment.Center;
            LineNumTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(LineNumTxt);
            Grid.SetRow(LineNumTxt, 0);
            Grid.SetColumn(LineNumTxt, 1);


            //package info
            TextBlock PackageInfoTxt = new TextBlock();
            PackageInfoTxt.Text = "Package Info: ";
            PackageInfoTxt.FontSize = 14;
            PackageInfoTxt.HorizontalAlignment = HorizontalAlignment.Center;
            PackageInfoTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(PackageInfoTxt);
            Grid.SetRow(PackageInfoTxt, 1);
            Grid.SetColumn(PackageInfoTxt, 1);
            Grid.SetColumnSpan(PackageInfoTxt, 3);

            //packageName
            TextBlock PackageNameTxt = new TextBlock();
            PackageNameTxt.Text = "Package : " + " to add pack name";
            PackageNameTxt.FontSize = 14;
            PackageNameTxt.HorizontalAlignment = HorizontalAlignment.Left;
            PackageNameTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(PackageNameTxt);
            Grid.SetRow(PackageNameTxt, 2);
            Grid.SetColumn(PackageNameTxt, 0);

            //minute
            TextBlock minuteTxt = new TextBlock();
            minuteTxt.Text = "Minutes used: " + receipt.UsedMinutes;
            minuteTxt.FontSize = 12;
            minuteTxt.HorizontalAlignment = HorizontalAlignment.Left;
            minuteTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(minuteTxt);
            Grid.SetRow(minuteTxt, 3);
            Grid.SetColumn(minuteTxt, 0);

            //minute left in package
            TextBlock minuteleftTxt = new TextBlock();
            minuteleftTxt.Text = "Minutes left in package: " + receipt.MinutesLeftInPackage;
            minuteleftTxt.FontSize = 12;
            minuteleftTxt.HorizontalAlignment = HorizontalAlignment.Left;
            minuteleftTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(minuteleftTxt);
            Grid.SetRow(minuteleftTxt, 3);
            Grid.SetColumn(minuteleftTxt, 1);

            //package %
            TextBlock minutePrecentTxt = new TextBlock();
            minutePrecentTxt.Text = "Minutes left in package: " + receipt.MinutesLeftInPackage;
            minutePrecentTxt.FontSize = 12;
            minutePrecentTxt.HorizontalAlignment = HorizontalAlignment.Left;
            minutePrecentTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(minutePrecentTxt);
            Grid.SetRow(minutePrecentTxt, 3);
            Grid.SetColumn(minutePrecentTxt, 2);

            //SMS
            TextBlock SmsTxt = new TextBlock();
            SmsTxt.Text = "SMS Used: " + receipt.UsedSMS;
            SmsTxt.FontSize = 12;
            SmsTxt.HorizontalAlignment = HorizontalAlignment.Left;
            SmsTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(SmsTxt);
            Grid.SetRow(SmsTxt, 4);
            Grid.SetColumn(SmsTxt, 0);

            //Smsleft
            TextBlock SmsLeftTxt = new TextBlock();
            SmsLeftTxt.Text = "SMS left in package: " + receipt.SMSLeftInPackage;
            SmsLeftTxt.FontSize = 12;
            SmsLeftTxt.HorizontalAlignment = HorizontalAlignment.Left;
            SmsLeftTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(SmsLeftTxt);
            Grid.SetRow(SmsLeftTxt, 4);
            Grid.SetColumn(SmsLeftTxt, 1);

            //SMS %
            TextBlock SmsPrecentTxt = new TextBlock();
            SmsPrecentTxt.Text = "SMS left in package: " + receipt.SMSLeftInPackage;
            SmsPrecentTxt.FontSize = 12;
            SmsPrecentTxt.HorizontalAlignment = HorizontalAlignment.Left;
            SmsPrecentTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(SmsPrecentTxt);
            Grid.SetRow(SmsPrecentTxt, 4);
            Grid.SetColumn(SmsPrecentTxt, 2);

            //price: 12123
            TextBlock PriceTxt = new TextBlock();
            PriceTxt.Text = "Price: " + receipt.PackagePrice;
            PriceTxt.FontSize = 12;
            PriceTxt.HorizontalAlignment = HorizontalAlignment.Center;
            LineNumTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(PriceTxt);
            Grid.SetRow(PriceTxt, 5);
            Grid.SetColumn(PriceTxt, 0);

            //out of package
            TextBlock OutOfPackTxt = new TextBlock();
            OutOfPackTxt.Text = "Out of Package";
            OutOfPackTxt.FontSize = 16;
            OutOfPackTxt.HorizontalAlignment = HorizontalAlignment.Left;
            OutOfPackTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(OutOfPackTxt);
            Grid.SetRow(OutOfPackTxt, 7);
            Grid.SetColumn(OutOfPackTxt, 0);

            //minute byond limit
            TextBlock MinBeyondLimitTxt = new TextBlock();
            MinBeyondLimitTxt.Text = "Minute beyond package limit: " + receipt.MinutesBeyondPackage;
            MinBeyondLimitTxt.FontSize = 12;
            MinBeyondLimitTxt.HorizontalAlignment = HorizontalAlignment.Left;
            MinBeyondLimitTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(MinBeyondLimitTxt);
            Grid.SetRow(MinBeyondLimitTxt, 8);
            Grid.SetColumn(MinBeyondLimitTxt, 0);

            //price per minute
            TextBlock PricePerMinTxt = new TextBlock();
            PricePerMinTxt.Text = "Price per minute: " + receipt.PricePerMinute;
            PricePerMinTxt.FontSize = 12;
            PricePerMinTxt.HorizontalAlignment = HorizontalAlignment.Left;
            PricePerMinTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(PricePerMinTxt);
            Grid.SetRow(PricePerMinTxt, 8);
            Grid.SetColumn(PricePerMinTxt, 1);

            //total minute price
            TextBlock TotalPriceMinTxt = new TextBlock();
            TotalPriceMinTxt.Text = "Total minute: " + receipt.TotalPaidMinutes;
            TotalPriceMinTxt.FontSize = 12;
            TotalPriceMinTxt.HorizontalAlignment = HorizontalAlignment.Left;
            TotalPriceMinTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(TotalPriceMinTxt);
            Grid.SetRow(TotalPriceMinTxt, 8);
            Grid.SetColumn(TotalPriceMinTxt, 2);

            //SMS beyond packege
            TextBlock SmsBeyondLimitTxt = new TextBlock();
            SmsBeyondLimitTxt.Text = "SMS beyond package limit: " + receipt.SMSBeyondPackage;
            SmsBeyondLimitTxt.FontSize = 12;
            SmsBeyondLimitTxt.HorizontalAlignment = HorizontalAlignment.Left;
            SmsBeyondLimitTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(SmsBeyondLimitTxt);
            Grid.SetRow(SmsBeyondLimitTxt, 9);
            Grid.SetColumn(SmsBeyondLimitTxt, 0);

            //price per sms
            TextBlock PricePerSmsTxt = new TextBlock();
            PricePerSmsTxt.Text = "Price per minute: " + receipt.PricePerMinute;
            PricePerSmsTxt.FontSize = 12;
            PricePerSmsTxt.HorizontalAlignment = HorizontalAlignment.Left;
            PricePerSmsTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(PricePerSmsTxt);
            Grid.SetRow(PricePerSmsTxt, 8);
            Grid.SetColumn(PricePerSmsTxt, 1);

            //total SMS price
            TextBlock TotalPriceSmsTxt = new TextBlock();
            TotalPriceSmsTxt.Text = "Total Sms: " + receipt.TotalPaidSMS;
            TotalPriceSmsTxt.FontSize = 12;
            TotalPriceSmsTxt.HorizontalAlignment = HorizontalAlignment.Left;
            TotalPriceSmsTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(TotalPriceSmsTxt);
            Grid.SetRow(TotalPriceSmsTxt, 9);
            Grid.SetColumn(TotalPriceSmsTxt, 2);

            //total
            TextBlock TotalPriceTxt = new TextBlock();
            TotalPriceTxt.Text = "Total price: " + receipt.TotalPayment;
            TotalPriceTxt.FontSize = 12;
            TotalPriceTxt.HorizontalAlignment = HorizontalAlignment.Left;
            TotalPriceTxt.VerticalAlignment = VerticalAlignment.Center;
            GridL.Children.Add(TotalPriceTxt);
            Grid.SetRow(TotalPriceTxt, 10);
            Grid.SetColumn(TotalPriceTxt, 1);


            return GridL;
        }
    }
}
