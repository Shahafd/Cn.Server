using CN.Common.Configs;
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
using System.Diagnostics;
using System.Net;
using System.Windows.Input;

namespace CN.Simulator.ViewModels
{
    class SimulatorViewModel : ISimulatorViewModel, INotifyPropertyChanged
    {
        ILogger logger { get; set; }
        IInputsValidator inputsValidator { get; set; }
        IHttpClient httpClient { get; set; }
        public ObservableCollection<string> Lines { get; private set; }
        public ICommand searchUserCommand { get; set; }
        public ICommand simulateCommand { get; set; }
        public string ClientId { get; set; }
        public int minDuration { get; set; }
        public int maxDuration { get; set; }
        public int numOfCalls { get; set; }
        public string destCall { get; set; }

        private string _selectedLine;

        public string selectedLine
        {
            get { return _selectedLine; }
            set { _selectedLine = value; Notify(nameof(selectedLine)); }
        }

        public ObservableCollection<string> Types { get; private set; }

        private string _SelectedType;
        public string SelectedType
        {
            get { return _SelectedType; }
            set { _SelectedType = value; Notify(nameof(SelectedType)); }
        }

        private bool _lockIdBox;

        public bool lockIdBox
        {
            get { return _lockIdBox; }
            set { _lockIdBox = value; Notify(nameof(lockIdBox)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public SimulatorViewModel(IHttpClient httpClient, ILogger logger, IInputsValidator inputsValidator)
        {
            this.logger = logger;
            this.inputsValidator = inputsValidator;
            this.httpClient = httpClient;
            this.lockIdBox = true;
            searchUserCommand = new ActionCommand(searchUser);
            simulateCommand = new ActionCommand<object>(simulate);

            Lines = new ObservableCollection<string> { "Please select a client first." };
            selectedLine = Lines[0];
            Types = new ObservableCollection<string> {
                "Call",
                "SMS"
            };
            SelectedType = Types[0];

        }

        private void simulate(object obj)
        {
            if (ValidateFields())
            {
                SimulatorAction simulatorAction = new SimulatorAction(ClientId, selectedLine, SelectedType, destCall, numOfCalls, minDuration, maxDuration);
                Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.SimulateRoute, simulatorAction);
                logger.Print(returnTuple.Item1.ToString());
                lockIdBox = true;
            }
        }


        private void searchUser()
        {
            string idValid = inputsValidator.ValidateStrInput("clientID", ClientId, InputsConfigs.MinIdLength, InputsConfigs.MaxIdLength);
            //send user id to server
            if (string.IsNullOrEmpty(idValid))
            {
                string s = ClientId;
                Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetClientByIdRoute, s);

                if (returnTuple.Item1 == null)
                {
                    logger.Print("Client not found!");
                    return;
                }
                JObject jobj = (JObject)returnTuple.Item1;
                Client client = jobj.ToObject<Client>();

                switch (returnTuple.Item2)
                {
                    case HttpStatusCode.OK:
                        //get lines
                        if (client != null)
                        {
                            GetClientLines(client.ID);
                            lockIdBox = false;
                        }
                        break;

                    case HttpStatusCode.Conflict:
                        logger.Print("Username or password are unvalid.");
                        break;

                    default:
                        logger.Print($"{returnTuple.Item2.ToString()} Error.");
                        break;
                }
            }
            else
            {
                logger.Print(idValid);
            }
        }

        public void GetClientLines(string clientId)
        {
            JObject jo = new JObject();
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetClientLinesRoute, clientId);
            JArray jobj = (JArray)returnTuple.Item1;
            List<Line> LineList = jobj.ToObject<List<Line>>();
            UpdateLines(LineList);
        }

        public void UpdateLines(List<Line> list)
        {
            Lines.Clear();
            foreach (var item in list)
            {
                Lines.Add(item.Number);
            }
        }


        private bool ValidateFields()
        {
            //validates the fields, return true if all fields are valid
            List<string> info = new List<string>();
            List<string> errors = new List<string>();
            if (selectedLine == null)
            {
                info.Add("Please select a line");
            }
            if (SelectedType == null)
            {
                info.Add("Please select type");
            }
            info.Add(inputsValidator.ValidateStrInput("client id", ClientId, InputsConfigs.MinIdLength, InputsConfigs.MaxIdLength));
            info.Add(inputsValidator.ValidateIntInput("minDuration", minDuration, 0, 299));
            info.Add(inputsValidator.ValidateIntInput("maxDuration", maxDuration, 0, 300));
            //validate if min >max
            if (maxDuration - minDuration <= 0)
            {
                errors.Add("Max duration must be bigger then Min duration");
            }

            info.Add(inputsValidator.ValidateIntInput("numOfCalls", numOfCalls, 1, InputsConfigs.MaxNumOfCalls));
            info.Add(inputsValidator.ValidatePhoneInput(destCall));

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

    }
}
