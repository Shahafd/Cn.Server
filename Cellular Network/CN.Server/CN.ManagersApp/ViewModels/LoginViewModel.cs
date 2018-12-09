using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Infrastructures;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.ManagersApp.Containers;
using CN.ManagersApp.Windows;
using Newtonsoft.Json;
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

namespace CN.ManagersApp.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand loginCommand { get; set; }
        public IInputsValidator inputsValidator { get; set; }
        ILogger logger { get; set; }
        IHttpClient httpClient { get; set; }
        IFileManager fileManager { get; set; }

        public LoginViewModel(ILogger logger, IHttpClient httpClient, IInputsValidator inputsValidator, IFileManager fileManager)
        {
            this.logger = logger;
            this.httpClient = httpClient;
            this.inputsValidator = inputsValidator;
            this.fileManager = fileManager;
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

                Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.ManagerLoginRoute, userLogin);

                switch (returnTuple.Item2)
                {
                    case HttpStatusCode.OK:
                        JObject jobj = (JObject)returnTuple.Item1;
                        User loggedIn = jobj.ToObject<User>();
                        UpdateSessionOnUserLogin(loggedIn);
                        CheckAndSendExceptions();
                        logger.Print($"Welcome Back {loggedIn.Username}!");
                        ManagersWindow managersWindow = new ManagersWindow(loggedIn);
                        managersWindow.Show();
                        CloseThisWindow();
                        break;
                    case HttpStatusCode.Unauthorized:
                        logger.Print("This system is for managers only.");
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
        private void CheckAndSendExceptions()
        {
            //checks if there are saved exceptions on the exceptions log file, and try to send them to the server
            List<Error> errors = new List<Error>();
            if (fileManager.FileExists(MainConfigs.ErrorsFile))
            {
                foreach (var item in fileManager.ReadFromFile(MainConfigs.ErrorsFile))
                {
                    Error error = JsonConvert.DeserializeObject<Error>(item);
                    errors.Add(error);
                }
                if (errors.Count > 0)
                {
                    Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.SendExceptionsListRoute, errors);
                    if (returnTuple.Item2 == HttpStatusCode.OK)
                    {
                        fileManager.ClearFile(MainConfigs.ErrorsFile);
                        logger.Print("Your last errors has been sent to the server, thanks for your support.");
                    }
                }
            }
        }

        private void UpdateSessionOnUserLogin(User loggedIn)
        {
            //updates the app on a loggedin user for exceptions handling

            ManContainer.container.GetInstance<ISessionData>().updateLoggedInID(loggedIn.ID);
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
