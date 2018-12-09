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
            cbObjects = new ObservableCollection<SelectableObject<Line>>();
            MonthList = new ObservableCollection<int>(MainConfigs.Months);
            YearList = new ObservableCollection<int>(MainConfigs.Years);
            CalculateCommand = new ActionCommand<object>(Calculate);
        }

        private void Calculate(object obj)
        {
            List<string> Checkedlines = new List<string>();
            foreach (var item in cbObjects)
            {
                if (item.IsSelected)
                {
                    Checkedlines.Add(item.ObjectData.Number);
                }
            }
            YearAndMonth selectedDate = new YearAndMonth(SelectedYear, SelectedMonth);
            BillRequestModel billRequestModel = new BillRequestModel(currentClient.ID, Checkedlines, selectedDate);
            Tuple<object, HttpStatusCode> returnTuple1 = httpClient.PostRequest(ApiConfigs.CheckIfLineExistedRoute, billRequestModel);
            if (returnTuple1.Item2 == HttpStatusCode.OK)
            {
                if ((bool)returnTuple1.Item1)
                {
                    Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetSpecficLinesBillRoute, billRequestModel);
                    if (returnTuple.Item2 == HttpStatusCode.OK)
                    {
                        JObject jObject = new JObject();
                        jObject = (JObject)returnTuple.Item1;
                        ClientBill clientBill = jObject.ToObject<ClientBill>();
                        BillWindow billWindow = new BillWindow(clientBill, selectedDate);
                        billWindow.Show();
                        CloseThisWindow();
                    }
                }
                else
                {
                    logger.Print("One or more of the selected lines didnt exist in the selected date");
                }
            }
            else
            {
                logger.Print(returnTuple1.Item2.ToString());
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


        public void OnCbObjectsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            comboBox.SelectedItem = null;
        }

        public void getClientFromWindow(Client client)
        {
            currentClient = client;
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



    }
}

