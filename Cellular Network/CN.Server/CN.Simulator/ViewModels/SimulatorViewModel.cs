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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CN.Simulator.ViewModels
{
    class SimulatorViewModel : ISimulatorViewModel, INotifyPropertyChanged
    {
        ILogger logger { get; set; }
        IInputsValidator inputsValidator { get; set; }
        public IHttpClient httpClient { get; set; }
        public ObservableCollection<string> Lines { get; private set; }
        public ICommand searchUserCommand { get; set; }
        public ICommand simulateCommand { get; set; }
        public string ClientId { get; set; }
        int minDuration { get; set; }
        int maxDuration { get; set; }
        int numOfCalls { get; set; }
        string destCall { get; set; }


        public SimulatorViewModel(IHttpClient httpClient, ILogger logger, IInputsValidator inputsValidator)
        {
            this.logger = logger;
            this.inputsValidator = inputsValidator;
            this.httpClient = httpClient;
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
            Debug.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            Debug.WriteLine("in searchUser");
            Debug.WriteLine(ClientId);
            string idValid = inputsValidator.ValidateStrInput("clientID", ClientId, InputsConfigs.MinIdLength, InputsConfigs.MaxIdLength);

            //send user id to server
            if (string.IsNullOrEmpty(idValid))
            {
                JObject j = new JObject();
                j = (JObject)httpClient.PostRequest(ApiConfigs.GetClientByIdRoute, ClientId);
                Client client = j.ToObject<Client>();
                Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");

                Console.WriteLine(client.ID);

                //get the user lines 
                if (client != null)
                {
                    Lines = (ObservableCollection<string>)httpClient.PostRequest(ApiConfigs.GetClientLinesRoute, client);
                }

            }
            else
            {
                logger.Print(idValid);
            }

        }

        //Methods:
        //get client  from DB(server) by client name (string)
        //get client lines from DB
        //
    }
}
