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
using System.Linq;
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
        IHttpClient httpClient { get; set; }

        public LoginViewModel(ILogger logger, IHttpClient httpClient)
        {
            this.logger = logger;
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
                JObject j = new JObject();
                j = (JObject)httpClient.PostRequest(ApiConfigs.LoginRoute, userLogin);
                User loggedIn = j.ToObject<User>();
                if (loggedIn != null)
                {
                    logger.Print($"Welcome Back {loggedIn.Username}!");
                    
                   
                    CloseThisWindow();
                }

            }
        }
        public bool ValidateFields()
        {
            //validates the fields
            List<string> validationInfo = new List<string>();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(Username))
            {
                validationInfo.Add("Please insert a username");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                validationInfo.Add("Please insert a password");
                valid = false;
            }
            logger.PrintList(validationInfo);
            return valid;
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
