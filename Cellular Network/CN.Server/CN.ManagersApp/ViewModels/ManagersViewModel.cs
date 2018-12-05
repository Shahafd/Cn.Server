using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Infrastructures;
using CN.Common.Models;
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

namespace CN.ManagersApp.ViewModels
{
    public class ManagersViewModel : IManagersViewModel, INotifyPropertyChanged
    {
        private User _loggedInUser;
        public User LoggedInUser
        {
            get { return _loggedInUser; }
            set { _loggedInUser = value; Notify(nameof(LoggedInUser)); }
        }
        private bool _mostValSwitch;
        public bool MostValSwitch
        {
            get { return _mostValSwitch; }
            set { _mostValSwitch = value; Notify(nameof(MostValSwitch)); }
        }
        private bool _mostCalledSwitch;
        public bool MostCalledSwitch
        {
            get { return _mostCalledSwitch; }
            set { _mostCalledSwitch = value; Notify(nameof(MostCalledSwitch)); }
        }
        private bool _bestSellersSwitch;
        public bool BestSellersSwitch
        {
            get { return _bestSellersSwitch; }
            set { _bestSellersSwitch = value; Notify(nameof(BestSellersSwitch)); }
        }
        private ObservableCollection<Client> _valueClients;
        public ObservableCollection<Client> ValueClients
        {
            get { return _valueClients; }
            set { _valueClients = value; Notify(nameof(ValueClients)); }
        }
        private ObservableCollection<Client> _callingClients;
        public ObservableCollection<Client> CallingClients
        {
            get { return _callingClients; }
            set { _callingClients = value; Notify(nameof(CallingClients)); }
        }
        private ObservableCollection<User> _bestSellers;
        public ObservableCollection<User> BestSellers
        {
            get { return _bestSellers; }
            set { _bestSellers = value; Notify(nameof(BestSellers)); }
        }

        IHttpClient httpClient { get; set; }
        ILogger logger { get; set; }
        public  ICommand mostValcommand { get; set; }
        public  ICommand mostCalledcommand { get; set; }
        public  ICommand bestSellerscommand { get; set; }


        public ManagersViewModel(IHttpClient httpClient,ILogger logger)
        {
            this.httpClient = httpClient;
            this.logger = logger;
            InitCollectionsAndSwitches();
            InitCommands();
            LoadCollections();
        }

        private void InitCommands()
        {
            //inits the commands
            mostValcommand = new ActionCommand(LoadValueClientsAction);
            mostCalledcommand = new ActionCommand(LoadMostCalledAction);
            bestSellerscommand = new ActionCommand(LoadBestSellersAction);
        }

        private void LoadBestSellersAction()
        {
            MostValSwitch = false;
            MostCalledSwitch = false;
            LoadBestSellers(true);
        }

        private void LoadMostCalledAction()
        {
            MostValSwitch = false;
            BestSellersSwitch = false;
            LoadMostCalledClients(true);
        }

        private void LoadValueClientsAction()
        {
            MostCalledSwitch = false;
            BestSellersSwitch = false;
            LoadValueClients(true);
        }

        private void LoadCollections()
        {
            //loads the collection from the server
            LoadValueClients(false);
            LoadMostCalledClients(false);
            LoadBestSellers(false);
        }

        private void LoadBestSellers(bool toSwitch)
        {
            //Loads the best sellers
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetBestSellersRoute);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JArray jarr = new JArray();
                jarr = (JArray)returnTuple.Item1;
                BestSellers.Clear();
                foreach (var item in jarr.ToObject<List<User>>())
                {
                    BestSellers.Add(item);
                }
            }
            else
            {
                logger.Print(returnTuple.Item2.ToString());
            }
            if (toSwitch)
            {
                BestSellersSwitch = true;
            }
        }

        private void LoadMostCalledClients(bool toSwitch)
        {
            //Loads the most called clients
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetMostCallingRoute);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JArray jarr = new JArray();
                jarr = (JArray)returnTuple.Item1;
                CallingClients.Clear();
                foreach (var item in jarr.ToObject<List<Client>>())
                {
                    CallingClients.Add(item);
                }
            }
            else
            {
                logger.Print(returnTuple.Item2.ToString());
            }
            if (toSwitch)
            {
                MostCalledSwitch = true;
            }
        }

        private void LoadValueClients(bool toSwitch)
        {
            //Loads the Valueable clients
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetValueClientsRoute);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JArray jarr = new JArray();
                jarr = (JArray)returnTuple.Item1;
                ValueClients.Clear();
                foreach (var item in jarr.ToObject<List<Client>>())
                {
                    ValueClients.Add(item);
                }
            }
            else
            {
                logger.Print(returnTuple.Item2.ToString());
            }
            if (toSwitch)
            {
                MostValSwitch = true;
            }
        }

        private void InitCollectionsAndSwitches()
        {
            //inits the collections and switches
            MostValSwitch = false;
            MostCalledSwitch = false;
            BestSellersSwitch = false;
            ValueClients = new ObservableCollection<Client>();
            CallingClients = new ObservableCollection<Client>();
            BestSellers = new ObservableCollection<User>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void GetUser(User user)
        {
            //gets the user from the window
            LoggedInUser = user;
        }

    }
}
