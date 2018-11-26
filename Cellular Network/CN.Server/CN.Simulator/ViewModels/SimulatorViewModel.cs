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

using System.Diagnostics;
using System.Net;
using System.Windows.Input;

namespace CN.Simulator.ViewModels
{
    class SimulatorViewModel : ISimulatorViewModel
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
        public string selectedLine { get; set; }
        public ObservableCollection<string> Types { get; private set; }
        public string SelectedType { get; set; }

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
            Types = new ObservableCollection<string> {
                "Call",
                "SMS"
            };

        }

        private void simulate(object obj)
        {
            Debug.WriteLine("clientID: " + ClientId);
            Debug.WriteLine("minduration: " + minDuration);
            Debug.WriteLine("Max duration: " + maxDuration);
            Debug.WriteLine("selectedline:: " + selectedLine);
            Debug.WriteLine("dest: " + destCall);
            Debug.WriteLine("numOfCalls" + numOfCalls);
            // logger.Print("simulate clicked!");

            //TODO: inputValidations
            inputsValidator.ValidateIntInput("minduration", minDuration.ToString(), InputsConfigs.SimulatorMinMinute, InputsConfigs.SimulatorMaxMinute);
            //wrong input validator
            SimulatorAction simulatorAction = new SimulatorAction(ClientId, selectedLine, SelectedType, destCall, numOfCalls, minDuration, maxDuration);

            //Tuple <obj=statuscode or string to show in logger,statuscode>
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.SimulateRoute, simulatorAction);
            //
            JObject jobj = (JObject)returnTuple.Item1;
            logger.Print(jobj.ToString());


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
                //UserLogin x = new UserLogin("9898989898", "0987893");
                String s = ClientId;

                Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetClientByIdRoute, s);
                // j = httpClient.PostRequest(ApiConfigs.GetClientByIdRoute, s);
                //Client client = j.ToObject<Client>();
                if (returnTuple.Item1 == null)
                {
                    logger.Print("Client not found!");
                    return;
                }



                JObject jobj = (JObject)returnTuple.Item1;
                Client client = jobj.ToObject<Client>();



                Debug.WriteLine(client.ID);

                switch (returnTuple.Item2)
                {
                    case HttpStatusCode.OK:
                        //get lines
                        if (client != null)
                        {
                            GetClientLines(client.ID);
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
            //(ObservableCollection<string>)httpClient.PostRequest(ApiConfigs.GetClientLinesRoute, client);
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

        //Methods:
        //get client  from DB(server) by client name (string)
        //get client lines from DB
        //
    }
}
