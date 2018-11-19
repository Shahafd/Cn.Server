using CN.Common.Configs;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CN.Simulator.ViewModels
{
    class SimulatorViewModel : ISimulatorViewModel, INotifyPropertyChanged
    {
        public IHttpClient httpClient { get; set; }
        public ObservableCollection<string> Lines { get; private set; }
        public ICommand searchUserCommand { get; set; }
        public ICommand simulateCommand { get; set; }
        private string _ClientId;

        public string ClientId
        {
            get { return _ClientId; }
            set { _ClientId = value; Notify(nameof(ClientId)); }
        }

        int minDuration { get; set; }
        int maxDuration { get; set; }
        int numOfCalls { get; set; }
        string destCall { get; set; }


        public SimulatorViewModel(IHttpClient httpClient)
        {
            searchUserCommand = new ActionCommand(searchUser);
            simulateCommand = new ActionCommand<object>(simulate);

            Lines = new ObservableCollection<string>
         {
            "test 1",
            "test 2"
            };

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void simulate(object obj)
        {
            throw new NotImplementedException();
        }

        private void searchUser()
        {
            //sed user id to server
            JObject j = new JObject();
            //Client client = j.ToObject<Client>();
            Client client = (Client)httpClient.PostRequest(ApiConfigs.LoginRoute, ClientId);

            //get the user lines 
            if (client != null)
            {
                Lines = (ObservableCollection<string>)httpClient.PostRequest(ApiConfigs.LoginRoute, client);
            }


        }

        //Methods:
        //get client  from DB(server) by client name (string)
        //get client lines from DB
        //
    }
}
