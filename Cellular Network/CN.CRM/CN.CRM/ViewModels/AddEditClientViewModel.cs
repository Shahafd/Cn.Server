using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Enums;
using CN.Common.Infrastructures;
using CN.Common.Models;
using CN.Common.Services;
using CN.CRM.Windows;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CN.CRM.ViewModels
{
    public class AddEditClientViewModel : IAddEditClientViewModel, INotifyPropertyChanged
    {
        private string _ID;
        public string ID
        {
            get { return _ID; }
            set { _ID = value; Notify(nameof(ID)); }
        }

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; Notify(nameof(FirstName)); }
        }
        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; Notify(nameof(LastName)); }
        }
        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; Notify(nameof(Address)); }
        }
        private ClientTypeEnum _ClientType;

        public ClientTypeEnum ClientType
        {
            get { return _ClientType; }
            set { _ClientType = value; Notify(nameof(ClientType)); }
        }
        private DateTime _BirthDate;

        public DateTime BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; Notify(nameof(BirthDate)); }
        }
        private string _ContactsNumber;
        public string ContactsNumber
        {
            get { return _ContactsNumber; }
            set { _ContactsNumber = value; Notify(nameof(ContactsNumber)); }
        }
        private bool _ExisitngClient;

        public bool ExisitngClient
        {
            get { return _ExisitngClient; }
            set { _ExisitngClient = value; Notify(nameof(ExisitngClient)); }
        }
        
        public ICommand submitCommand { get; set; }
        public ICommand deleteCommand { get; set; }
        public ICommand clearCommand { get; set; }
        public IInputsValidator inputsValidator { get; set; }
        public IHttpClient httpClient { get; set; }
        public ILogger logger { get; set; }
        public ICrmViewModel crmViewModel { get; set; }
        public AddEditClientViewModel(IInputsValidator inputsValidator, IHttpClient httpClient, ILogger logger, ICrmViewModel crmViewModel)
        {
            BirthDate = DateTime.Now.AddDays(1);
            ExisitngClient = false;
            this.inputsValidator = inputsValidator;
            this.httpClient = httpClient;
            this.logger = logger;
            this.crmViewModel = crmViewModel;
            InitCommands();
        }

        private void InitCommands()
        {
            //inits the commands
            submitCommand = new ActionCommand(SendClient);
            deleteCommand = new ActionCommand(DeleteClient);
            clearCommand = new ActionCommand(ClearFields);
        }

        private void ClearFields()
        {
            //clears the fields

            FirstName = "";
            LastName = "";
            Address = "";
            ClientType = ClientTypeEnum.Private;
            BirthDate = DateTime.Now;
            if (!ExisitngClient)
            {
                ID = "";
                ContactsNumber = "";
            }
        }

        private void DeleteClient()
        {
            //deletes the client
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.DeleteClientRoute, ID);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                logger.Print(returnTuple.Item1.ToString());
                crmViewModel.LoadClients();
                CloseThisWindow();
            }
            else
            {
                logger.Print($"{returnTuple.Item2.ToString()} Error.");
            }
        }

        public void UpdateExisiting(Client client)
        {
            //updates the viewmodel that the client is elready exists and fills the fields with his details
            ExisitngClient = true;
            ID = client.ID;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Address = client.Address;
            ClientType = client.ClientType;
            BirthDate = client.BirthDate;
            ContactsNumber = client.ContactNumber;

        }
        private void SendClient()
        {
            //verify the fields, if all are valid sends the model to the server
            if (VerifyFields())
            {
                Client client = new Client(ID, FirstName, LastName, ClientType, Address, ContactsNumber, BirthDate);
                if (ExisitngClient)
                {
                    TryUpdateExisitingClient(client);
                }
                else
                {
                    TryCreateNewClient(client);
                }
            }
        }

        private void TryCreateNewClient(Client client)
        {
            //tries to create a new client
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.IsClientIdExistsRoute, ID);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                bool idExists = Convert.ToBoolean(returnTuple.Item1);
                if (idExists)
                {
                    logger.Print($"The Id {ID} already exists.");
                    return;
                }
                else
                {
                    Tuple<object, HttpStatusCode> returnTuple2 = httpClient.PostRequest(ApiConfigs.AddClientRoute, client);
                    if (returnTuple2.Item2 == HttpStatusCode.OK)
                    {
                        JArray jarr = new JArray();
                        jarr = (JArray)returnTuple2.Item1;
                        List<string> errors = new List<string>();
                        foreach (var item in jarr.ToObject<List<string>>())
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                errors.Add(item);
                            }
                        }
                        if (errors.Count == 0)
                        {
                            logger.Print("Client Added successfuly!");
                            crmViewModel.LoadClients();
                            CloseThisWindow();
                        }
                        else
                        {
                            logger.PrintList(errors);
                        }
                    }
                    else
                    {
                        logger.Print($"{returnTuple2.Item2.ToString()} Error.");
                    }
                }
            }
        }

        private void TryUpdateExisitingClient(Client client)
        {
            //tries to update an exisitng client
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.UpdateExisitngClientRoute, client);
            switch (returnTuple.Item2)
            {

                case HttpStatusCode.OK:
                    if (string.IsNullOrEmpty(returnTuple.Item1.ToString()))
                    {
                        logger.Print("Client Details updated successfuly!");
                        crmViewModel.LoadClients();
                        CloseThisWindow();
                    }
                    else
                    {
                        logger.Print(returnTuple.Item1.ToString());
                    }
                    break;
                default:
                    logger.Print($"{returnTuple.Item2.ToString()} Error.");
                    break;
            }

        }

        private bool VerifyFields()
        {
            List<string> validations = new List<string>();
            validations.Add(inputsValidator.ValidateIDInput("ID", ID));
            validations.Add(inputsValidator.ValidateStrInput("First Name", FirstName, InputsConfigs.MinGenInputLength, InputsConfigs.MaxGenInputLength));
            validations.Add(inputsValidator.ValidateStrInput("Last Name", LastName, InputsConfigs.MinGenInputLength, InputsConfigs.MaxGenInputLength));
            validations.Add(inputsValidator.ValidateStrInput("Address", Address, InputsConfigs.MinGenInputLength, InputsConfigs.MaxGenInputLength));
            validations.Add(inputsValidator.ValidatePhoneInput(ContactsNumber));
            validations.Add(inputsValidator.ValidateDateInput("Birth Date", BirthDate));

            List<string> errors = new List<string>();
            foreach (var item in validations)
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private void CloseThisWindow()
        {
            //closes this window
            for (int i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i].GetType() == typeof(AddEditClientWindow))
                {
                    Application.Current.Windows[i].Close();
                }
            }
        }
    }
}
