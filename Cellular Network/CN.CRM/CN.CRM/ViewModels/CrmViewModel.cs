using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Enums;
using CN.Common.Infrastructures;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.CRM.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CN.CRM.ViewModels
{
    public class CrmViewModel : ICrmViewModel, INotifyPropertyChanged
    {
        public User loggedInUser { get; set; }
        public IHttpClient httpClient { get; set; }
        ILogger logger { get; set; }
        public ICommand newClientCommand { get; set; }
        public ICommand editDeleteClientCommand { get; set; }
        public ICommand newLineCommand { get; set; }
        public ICommand editDeleteLinesCommand { get; set; }
        public ICommand logoutCommand { get; set; }
        public ICommand ReceipetCommand { get; set; }
        private string _searchInput;

        public string searchInput
        {
            get { return _searchInput; }
            set { _searchInput = value; SearchInputChanged(searchInput); }
        }

        private string _myDetails;
        public string MyDetails
        {
            get { return _myDetails; }
            set { _myDetails = value; Notify(nameof(MyDetails)); }
        }

        private Client _selectedClient;
        public Client selectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; Notify(nameof(selectedClient)); }
        }
        public void SearchInputChanged(string input)
        {
            //search for a client that one of it fields matches the search input
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.ClientSearchRoute, input);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JArray jarr = new JArray();
                jarr = (JArray)returnTuple.Item1;
                List<Client> clients = jarr.ToObject<List<Client>>();
                UpdateClientsFromSearch(clients);
            }
            else
            {
                logger.Print($"{returnTuple.Item2.ToString()} Error.");
            }
        }

        private void UpdateClientsFromSearch(List<Client> clients)
        {
            //updates the observable collection after recieved from the search 
            Clients.Clear();
            foreach (var item in clients)
            {
                Clients.Add(item);
            }
        }
        private ObservableCollection<Client> _Clients;
        public ObservableCollection<Client> Clients
        {
            get { return _Clients; }
            set { _Clients = value; Notify(nameof(Clients)); }
        }

        public CrmViewModel(ILogger logger, IHttpClient httpClient)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            InitCollections();
            InitCommands();
            LoadClients();
            searchInput = "";
        }

        private void InitCommands()
        {
            //inits the commands
            newClientCommand = new ActionCommand(OpenNewClientWindow);
            editDeleteClientCommand = new ActionCommand(OpenEditDeleteClientWindow);
            newLineCommand = new ActionCommand(OpenNewLineWindow);
            editDeleteLinesCommand = new ActionCommand(OpenLinesWindow);
            logoutCommand = new ActionCommand(Logout);
            ReceipetCommand = new ActionCommand(CreateReceipt);

        }

        private void CreateReceipt()
        {
            //opens a new recepit window 
            BillDatePicker billDatePicker = new BillDatePicker(selectedClient);
            billDatePicker.Show();
        }

        private void Logout()
        {
            //logs out of the crm
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            CloseThisWindow();
        }

        private void CloseThisWindow()
        {
            //closes this window
            for (int i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i].GetType() == typeof(CrmWindow))
                {
                    Application.Current.Windows[i].Close();
                }
            }
        }

        private void OpenNewLineWindow()
        {
            //opens a new line window for the selected client
            AddEditLinesWindow addEditLineWindow = new AddEditLinesWindow(selectedClient, true, loggedInUser);
            addEditLineWindow.Show();
        }
        private void OpenLinesWindow()
        {
            //opens a lines window for editing and deleting for the selected client
            AddEditLinesWindow addEditLineWindow = new AddEditLinesWindow(selectedClient, false);
            if (!addEditLineWindow.viewModel.HasNoLines(selectedClient))
            {
                addEditLineWindow.Show();
            }
            else
            {
                logger.Print("Client has no lines, being redirected to new line window. ");
            }
        }

        private void OpenEditDeleteClientWindow()
        {
            //opens the selected client's details window
            AddEditClientWindow addEditClientWindow = new AddEditClientWindow(selectedClient);
            addEditClientWindow.Show();
        }

        private void OpenNewClientWindow()
        {
            //opens the new client window
            AddEditClientWindow addEditClientWindow = new AddEditClientWindow(null);
            addEditClientWindow.Show();
        }

        private void InitCollections()
        {
            //inits the collections
            Clients = new ObservableCollection<Client>();

        }

        public void UpdateUser(User user)
        {
            //updates the user from the window
            loggedInUser = user;
            MyDetails = $"Hello, {loggedInUser.Username}";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void LoadClients()
        {
            //loads the clients
            Clients.Clear();
            JArray jarr = new JArray();
            Tuple<object, HttpStatusCode> returnTuple = httpClient.GetRequest(ApiConfigs.GetAllClientsRoute);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                jarr = (JArray)returnTuple.Item1;
                foreach (var item in jarr.ToObject<List<Client>>())
                {
                    Clients.Add(item);
                }
            }



        }
    }
}
