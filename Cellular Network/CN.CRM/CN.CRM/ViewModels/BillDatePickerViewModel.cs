using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Infrastructures;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.CRM.Windows;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CN.CRM.ViewModels
{
    public class BillDatePickerViewModel : IBillDatePickerViewModel
    {
        IHttpClient httpClient { get; set; }
        ILogger logger { get; set; }
        public ObservableCollection<SelectableObject<Line>> cbObjects { get; set; }
        public ObservableCollection<int> MonthList { get; set; }
        public int SelectedMonth { get; set; }
        public ObservableCollection<int> YearList { get; set; }
        public int SelectedYear { get; set; }
        public ICommand CalculateCommand { get; set; }
        public Client currentClient;


        public BillDatePickerViewModel(ILogger logger, IHttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            cbObjects = new ObservableCollection<SelectableObject<Line>>();// { new SelectableObject<string>("111", false), new SelectableObject<string>("222", false), new SelectableObject<string>("333", false) };
            MonthList = new ObservableCollection<int>(MainConfigs.Months);
            YearList = new ObservableCollection<int>(MainConfigs.Years);
            CalculateCommand = new ActionCommand<object>(Calculate);
        }

        private void Calculate(object obj)
        {
            // logger.Print("Calculate"+SelectedMonth+"   "+SelectedYear +"  "+currentClient.ID);
            List<string> Checkedlines = new List<string>();
            foreach (var item in cbObjects)
            {
                if (item.IsSelected)
                {
                    Checkedlines.Add(item.ObjectData.Number);
                }
            }
            BillRequestModel billRequestModel = new BillRequestModel(currentClient.ID, Checkedlines, new YearAndMonth(SelectedYear, SelectedMonth));
            // logger.Print("checekd lines: "+Checkedlines[0]+"  "+Checkedlines[1]);
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetSpecficLinesBillRoute, billRequestModel);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                //ClientBill clientBill = new ClientBill() ;
                // //Newtonsoft.Json.JsonSerializer d=new Newtonsoft.Json.JsonSerializer();
                JObject jObject = new JObject();
                JArray jArray = new JArray();
                JObject jObjectP = new JObject();
                JObject jObjectPD = new JObject();

                jObject = (JObject)returnTuple.Item1;
                jArray = (JArray)jObject["Recepits"];//need jarray
                                                     // foreach (var item in jArray)
                                                     // {
                                                     ////    jObjectP =(JObject) item["Package"];
                                                     //    // jObjectPD = (JObject)item["PackegeDetails"];
                                                     //     //Receipt receipt = item.ToObject<Receipt>();
                                                     //    // logger.Print(receipt.LineNumber);    
                                                     // }

                List<Receipt> receipts = jArray.ToObject<List<Receipt>>();//add jobjects: packdetails ,package
                                                                          // // clientBill = (ClientBill)jObject.ToObject(clientBill.GetType(),d);
                ClientBill clientBill = jObject.ToObject<ClientBill>();
                BillWindow billWindow = new BillWindow(clientBill);
                billWindow.Show();
                CloseThisWindow();
            }

        }
        private void CloseThisWindow()
        {
            //closes this window
            for (int i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i].GetType() == typeof(BillDatePicker))
                {
                    Application.Current.Windows[i].Close();
                }
            }
        }
        /////////////////////////////////////////////////////////
        public void OnCbObjectCheckBoxChecked(object sender, EventArgs e)
        {
            //logger.Print("in viewmodel-OnCbObjectCheckBoxChecked ");
            //  SelectableObject<string> s = (SelectableObject<string>)sender;

        }
        public void OnCbObjectCheckBoxUnChecked(object sender, EventArgs e)
        {
            //logger.Print("in viewmodel-OnCbObjectCheckBoxChecked ");


        }

        public void OnCbObjectsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.SelectedItem = null;
        }

        public void getClientFromWindow(Client client)
        {
            this.currentClient = client;
            GetClientLines(client.ID);
        }
        public void GetClientLines(string clientId)
        {

            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetClientLinesRoute, clientId);
            JArray jobj = (JArray)returnTuple.Item1;
            List<Line> LineList = jobj.ToObject<List<Line>>();
            UpdateLines(LineList);
        }

        public void UpdateLines(List<Line> list)
        {
            cbObjects.Clear();
            foreach (var item in list)
            {
                cbObjects.Add(new SelectableObject<Line>(item, false));
            }
        }

        ////////////////////////////////////////////////////////






        ////////////////////////////////////////////////
        public class SelectableObject<T>
        {
            public bool IsSelected { get; set; }
            public T ObjectData { get; set; }

            public SelectableObject(T objectData)
            {
                ObjectData = objectData;
            }

            public SelectableObject(T objectData, bool isSelected)
            {
                IsSelected = isSelected;
                ObjectData = objectData;
            }
        }
        /////////////////////////////////////////////////////////

    }
}

