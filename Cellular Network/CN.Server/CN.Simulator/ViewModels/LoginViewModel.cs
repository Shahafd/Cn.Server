using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Enums;
using CN.Common.Infrastructures;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.Simulator.Windows;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CN.Simulator.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand loginCommand { get; set; }
        ILogger logger { get; set; }
        IInputsValidator inputsValidator { get; set; }
        IHttpClient httpClient { get; set; }

        public LoginViewModel(ILogger logger, IHttpClient httpClient, IInputsValidator inputsValidator)
        {
            this.logger = logger;
            this.inputsValidator = inputsValidator;
            this.httpClient = httpClient;
            loginCommand = new ActionCommand<object>(TryLogin);
            Username = "Shahaf";
        }

        private void TryLogin(object parameter)
        {
            //Sends the info to the server and tries a login
            PasswordBox psbox = (PasswordBox)parameter;
            Password = psbox.Password;
            if (ValidateFields())
            {
                UserLogin userLogin = new UserLogin(Username, Password);

                Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.LoginRoute, userLogin);
                JObject jobj = (JObject)returnTuple.Item1;
                User loggedIn = jobj.ToObject<User>();

                switch (returnTuple.Item2)
                {
                    case HttpStatusCode.OK:
                        logger.Print($"Welcome Back {loggedIn.Username}!");
                        SimulatorWindow simulatorWindow = new SimulatorWindow(loggedIn);
                        simulatorWindow.Show();
                        CloseThisWindow();
                        break;

                    case HttpStatusCode.Conflict:
                        logger.Print("Username or password are unvalid.");
                        break;

                    default:
                        logger.Print($"{returnTuple.Item2.ToString()} Error.");
                        break;
                }
            }
        }
        public bool ValidateFields()
        {
            //validates the fields
            List<string> validations = new List<string>();
            validations.Add(inputsValidator.ValidateStrInput("Username", Username, 2, 10));
            validations.Add(inputsValidator.ValidateStrInput("Password", Password, 2, 10));
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

        private void CloseOtherWindows()
        {
            //closes other windows
            for (int i = App.Current.Windows.Count - 1; i > 1; i--)
                App.Current.Windows[i].Close();
        }
        private void CloseThisWindow()
        {
            //closes this window
            for (int i = 0; i < Application.Current.Windows.Count; i++)
            {
                if (Application.Current.Windows[i].GetType() == typeof(MainWindow))
                {
                    Application.Current.Windows[i].Close();
                }
            }
        }
    }
}
