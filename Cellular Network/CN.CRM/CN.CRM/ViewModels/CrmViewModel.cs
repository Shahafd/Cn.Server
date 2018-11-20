using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Enums;
using CN.Common.Infrastructures;
using CN.Common.Models;
using CN.CRM.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CN.CRM.ViewModels
{
    public class CrmViewModel : ICrmViewModel, INotifyPropertyChanged
    {
        public User loggedInUser { get; set; }
        public IHttpClient httpClient { get; set; }
        ILogger logger { get; set; }
        public ICommand newClientCommand { get; set; }

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
        }

        private void InitCommands()
        {
            //inits the commands
            newClientCommand = new ActionCommand(OpenClientDetailsWindow);
        }

        private void OpenClientDetailsWindow()
        {
            //opens the new client window
            AddEditClientWindow addEditClientWindow = new AddEditClientWindow(selectedClient);
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
    }
}
