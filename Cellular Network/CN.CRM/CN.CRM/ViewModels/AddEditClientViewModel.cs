using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Enums;
using CN.Common.Infrastructures;
using CN.Common.Models;
using CN.Common.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CN.CRM.ViewModels
{
    public class AddEditClientViewModel : IAddEditClientViewModel
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public bool ExisitngClient { get; set; }

        public ICommand submitCommand { get; set; }
        public IInputsValidator inputsValidator { get; set; }
        public IHttpClient httpClient { get; set; }
        public ILogger logger { get; set; }
        public AddEditClientViewModel(IInputsValidator inputsValidator, IHttpClient httpClient, ILogger logger)
        {
            ExisitngClient = false;
            this.inputsValidator = inputsValidator;
            this.httpClient = httpClient;
            this.logger = logger;
            submitCommand = new ActionCommand(SendClient);
        }
        public void UpdateExisiting()
        {
            //updates the viewmodel that the client is elready exists
            ExisitngClient = true;
        }
        private void SendClient()
        {
            //verify the fields, if all are valid sends the model to the server
            if (VerifyFields())
            {
                JObject j = new JObject();
                Client client = new Client(ID, FirstName, LastName, (int)ClientType, Address, ContactNumber, BirthDate);
                if (ExisitngClient)
                {
                    //lock id textbox
                    j = (JObject)httpClient.PostRequest(ApiConfigs.UpdateExistingClientRoute, client);
                    RequestStatusEnum status = j.ToObject<RequestStatusEnum>();
                    if (status == RequestStatusEnum.Success)
                    {
                        logger.Print("Client Details updated successfuly!");
                        //close window
                    }
                }
                else
                {
                string error= httpClient.PostRequest(ApiConfigs.AddClientRoute, client).ToString();
                   
                    if (string.IsNullOrEmpty(error))
                    {
                        logger.Print("Client Added successfuly!");
                        //close window
                    }
                    else
                    {
                        logger.Print(error);
                    }
                }

            }
        }

        private bool VerifyFields()
        {
            List<string> validations = new List<string>();
            validations.Add(inputsValidator.ValidateIDInput("ID", ID));
            validations.Add(inputsValidator.ValidateStrInput("First Name", FirstName, InputsConfigs.MinGenInputLength, InputsConfigs.MaxGenInputLength));
            validations.Add(inputsValidator.ValidateStrInput("Last Name", LastName, InputsConfigs.MinGenInputLength, InputsConfigs.MaxGenInputLength));
            validations.Add(inputsValidator.ValidateStrInput("Address", Address, InputsConfigs.MinGenInputLength, InputsConfigs.MaxGenInputLength));
            validations.Add(inputsValidator.ValidatePhoneInput(ContactNumber));

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
    }
}
