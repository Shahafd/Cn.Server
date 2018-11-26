﻿using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Infrastructures;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CN.CRM.ViewModels
{
    public class AddEditLineViewModel : IAddEditLineViewModel, INotifyPropertyChanged
    {
        private bool _ExistingLine;
        public bool ExistingLine
        {
            get { return _ExistingLine; }
            set { _ExistingLine = value; Notify(nameof(ExistingLine)); }
        }
        private SelectedNumbers _selectedNumbers;
        public SelectedNumbers SelectedNumbers
        {
            get { return _selectedNumbers; }
            set { _selectedNumbers = value; Notify(nameof(SelectedNumbers)); }
        }
        private PackageDetails _selectedPackageDetails;
        public PackageDetails SelectedPackageDetails
        {
            get { return _selectedPackageDetails; }
            set { _selectedPackageDetails = value; Notify(nameof(SelectedPackageDetails)); }
        }
        private ObservableCollection<string> _Lines;
        public ObservableCollection<string> Lines
        {
            get { return _Lines; }
            set { _Lines = value; Notify(nameof(Lines)); }
        }
        private ObservableCollection<Package> _Packages;
        public ObservableCollection<Package> Packages
        {
            get { return _Packages; }
            set { _Packages = value; Notify(nameof(Packages)); }
        }
        private string _SelectedLine;
        public string SelectedLine
        {
            get { return _SelectedLine; }
            set { _SelectedLine = value; SelectedLineChanged(); }
        }
        private Package _SelectedPackage;
        public Package SelectedPackage
        {
            get { return _SelectedPackage; }
            set { _SelectedPackage = value; SelectedPackageChanged(); }
        }
        private int _Minutes;
        public int Minutes
        {
            get { return _Minutes; }
            set { _Minutes = value; Notify(nameof(Minutes)); }
        }
        private int _SMS;
        public int SMS
        {
            get { return _SMS; }
            set { _SMS = value; Notify(nameof(SMS)); }
        }
        private double _MinutePrice;
        public double MinutePrice
        {
            get { return _MinutePrice; }
            set { _MinutePrice = value; Notify(nameof(MinutePrice)); }
        }
        private double _SMSPrice;
        public double SMSPrice
        {
            get { return _SMSPrice; }
            set { _SMSPrice = value; Notify(nameof(SMSPrice)); }
        }
        private double _Discount;
        public double Discount
        {
            get { return _Discount; }
            set { _Discount = value; Notify(nameof(Discount)); }
        }
        private string _SelectedNum1;
        public string SelectedNum1
        {
            get { return _SelectedNum1; }
            set { _SelectedNum1 = value; Notify(nameof(SelectedNum1)); }
        }
        private string _SelectedNum2;
        public string SelectedNum2
        {
            get { return _SelectedNum2; }
            set { _SelectedNum2 = value; Notify(nameof(SelectedNum2)); }
        }
        private string _SelectedNum3;
        public string SelectedNum3
        {
            get { return _SelectedNum3; }
            set { _SelectedNum3 = value; Notify(nameof(SelectedNum3)); }
        }
        private string _MostCalledNum;
        public string MostCalledNum
        {
            get { return _MostCalledNum; }
            set { _MostCalledNum = value; Notify(nameof(MostCalledNum)); }
        }
        private double _TotalPrice;

        public double TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value;Notify(nameof(TotalPrice)); }
        }


        public IHttpClient httpClient { get; set; }
        public IInputsValidator inputsValidator { get; set; }
        public ILogger logger { get; set; }
        public ICommand submitCommand { get; set; }
        public AddEditLineViewModel(IHttpClient httpClient, IInputsValidator inputsValidator, ILogger logger)
        {
            this.httpClient = httpClient;
            this.inputsValidator = inputsValidator;
            this.logger = logger;
            InitCollections();
            InitCommands();
        }

        private void InitCommands()
        {
            //inits the commands
            submitCommand = new ActionCommand(SendLine);
        }

        private void SendLine()
        {
            //validates the fields and send the line,package and details
            if (ValidateFields())
            {
                SelectedPackageDetails.MaxMinutes = Minutes;
                SelectedPackageDetails.MaxSMS = SMS;
                SelectedPackageDetails.FixedCallPrice = MinutePrice;
                SelectedPackageDetails.FixedSmsPrice = SMSPrice;
                SelectedPackageDetails.DiscountPercentage = Discount;
                SelectedPackageDetails.MostCalledNumber = MostCalledNum;
                SelectedNumbers.FirstNumber = SelectedNum1;
                SelectedNumbers.SecondNumber = SelectedNum2;
                SelectedNumbers.ThirdNumber = SelectedNum3;
                SelectedPackage.DefaultPackage = false;
                SelectedPackage.PackageTotalPrice = TotalPrice;
                LinePackObject linePackObj = new LinePackObject(SelectedLine, SelectedPackage, SelectedPackageDetails, SelectedNumbers);
                Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.SendLinePackageRoute, linePackObj);
            }
        }

        private bool ValidateFields()
        {
            //validates the fields, return true if all fields are valid
            List<string> info = new List<string>();
            List<string> errors = new List<string>();
            if (SelectedLine == null)
            {
                info.Add("Please select a line");
            }
            if (SelectedPackage == null)
            {
                info.Add("Please select a package");
            }
            info.Add(inputsValidator.ValidateIntInput("Minutes Amount", Minutes, 0, PricesConfigs.MaxMinutesInPackage));
            info.Add(inputsValidator.ValidateIntInput("SMS Amount", SMS, 0, PricesConfigs.MaxSMSInPackage));
            info.Add(inputsValidator.ValidateDoubleInput("Miuntes Price", MinutePrice, 0, PricesConfigs.MaxMinutePrice));
            info.Add(inputsValidator.ValidateDoubleInput("SMS Price", SMSPrice, 0, PricesConfigs.MaxSMSPrice));
            info.Add(inputsValidator.ValidateDoubleInput("Discount %", Discount, 0, PricesConfigs.MaxDiscountPrecentage));
            info.Add(inputsValidator.ValidateDoubleInput("Package Total Price", TotalPrice, PricesConfigs.MinPackagePrice, PricesConfigs.MaxPackagePrice));
            if (!string.IsNullOrEmpty(SelectedNum1))
            {
                info.Add(inputsValidator.ValidatePhoneInput(SelectedNum1));
            }
            if (!string.IsNullOrEmpty(SelectedNum2))
            {
                info.Add(inputsValidator.ValidatePhoneInput(SelectedNum2));
            }
            if (!string.IsNullOrEmpty(SelectedNum3))
            {
                info.Add(inputsValidator.ValidatePhoneInput(SelectedNum3));
            }
            if (!string.IsNullOrEmpty(MostCalledNum))
            {
                info.Add(inputsValidator.ValidatePhoneInput(MostCalledNum));
            }
            foreach (var item in info)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    errors.Add(item);
                }
            }
            if (errors.Count == 0)
            {
                return true;
            }
            else
            {
                logger.PrintList(errors);
                return false;
            }
        }


        private void InitCollections()
        {
            //inits the collections
            Lines = new ObservableCollection<string>();
            Packages = new ObservableCollection<Package>();
        }

        private void SelectedPackageChanged()
        {
            //updates the packages field with the selected package's details
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetPackageDetails, SelectedPackage.ID);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JObject jobj = new JObject();
                jobj = (JObject)returnTuple.Item1;
                SelectedPackageDetails = jobj.ToObject<PackageDetails>();
                if (!SelectedPackage.DefaultPackage)
                {
                    GetSelectedNumbers();
                    UpdateFields(false);
                }
                else
                {
                    UpdateFields(true);
                }

            }
            else
            {
                logger.Print($"{returnTuple.Item2.ToString()} Error.");
            }

        }

        private void GetSelectedNumbers()
        {
            //gets the selected numbers from the selected package's details
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetSelectedNumbersRoute, SelectedPackageDetails.SelectedNumbersID);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JObject jobj = new JObject();
                jobj = (JObject)returnTuple.Item1;
                SelectedNumbers = jobj.ToObject<SelectedNumbers>();
            }
            else
            {
                logger.Print(returnTuple.Item2.ToString());
            }
        }

        private void UpdateFields(bool defaultPackage)
        {
            //updates the fields of the viewModel
            Minutes = SelectedPackageDetails.MaxMinutes;
            SMS = SelectedPackageDetails.MaxSMS;
            MinutePrice = SelectedPackageDetails.FixedCallPrice;
            SMSPrice = SelectedPackageDetails.FixedSmsPrice;
            Discount = SelectedPackageDetails.DiscountPercentage;
            TotalPrice = SelectedPackage.PackageTotalPrice;
            if (!defaultPackage)
            {
                SelectedNum1 = SelectedNumbers.FirstNumber;
                SelectedNum2 = SelectedNumbers.SecondNumber;
                SelectedNum3 = SelectedNumbers.ThirdNumber;
                MostCalledNum = SelectedPackageDetails.MostCalledNumber;
            }

        }

        private void SelectedLineChanged()
        {
            //the client selected a line
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetPackageByLineRoute, SelectedLine);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JObject jobj = new JObject();
                jobj = (JObject)returnTuple.Item1;
                SelectedPackage = jobj.ToObject<Package>();
                Packages.Clear();
                Packages.Add(SelectedPackage);
            }
            else
            {
                logger.Print($"{returnTuple.Item2.ToString()} Error.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void GetData(Client client, bool newLine)
        {
            //gets the data sent from the view
            ExistingLine = !newLine;
            if (newLine)
            {
                GetNewLineNumber(client);
            }
            else
            {
                GetClientLines(client);
            }

        }

        private void GetClientLines(Client client)
        {
            //gets the lines owned by this client
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetClientLinesStrRoute, client.ID);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JArray jarr = new JArray();
                jarr = (JArray)returnTuple.Item1;
                List<string> clientLines = jarr.ToObject<List<string>>();
                Lines.Clear();
                foreach (var item in clientLines)
                {
                    Lines.Add(item);
                }
            }
            else
            {
                logger.Print($"{returnTuple.Item2.ToString()} Error.");
            }

        }

        private void GetNewLineNumber(Client client)
        {
            //gets a new number for the client or his contacts number if he doesnt owns any lines yet
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetNewLine, client);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                Lines.Add(returnTuple.Item1.ToString());
                SelectedLine = returnTuple.Item1.ToString();

                GetDefaultPackages();
            }
            else
            {
                logger.Print(returnTuple.Item2.ToString());
            }
        }

        private void GetDefaultPackages()
        {
            //returns the default packages to the client
            Tuple<object, HttpStatusCode> returnTuple = httpClient.GetRequest(ApiConfigs.GetDefaultPackagesRoute);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JArray jarr = new JArray();
                jarr = (JArray)returnTuple.Item1;
                Packages = jarr.ToObject<ObservableCollection<Package>>();
            }
            else
            {
                logger.Print($"{returnTuple.Item2.ToString()} Error.");
            }
        }
    }
}
