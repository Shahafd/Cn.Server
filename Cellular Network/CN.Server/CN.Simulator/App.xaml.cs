using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Models.TempModels;
using CN.Simulator.Containers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace CN.Simulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        IHttpClient httpClient { get; set; }
        ILogger logger { get; set; }
        IFileManager fileManager { get; set; }
        ISessionData sessionData { get; set; }
        public App()
        {
            httpClient = SimulatorContainer.container.GetInstance<IHttpClient>();
            logger = SimulatorContainer.container.GetInstance<ILogger>();
            fileManager = SimulatorContainer.container.GetInstance<IFileManager>();
            sessionData = SimulatorContainer.container.GetInstance<ISessionData>();

        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(AppDispatcherUnhandledException);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (MainConfigs.CatchExceptions)
            {
                //handles the exceptions
                e.Handled = true;
                Error error = new Error(e.Exception, sessionData.GetLoggedInID());
                Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.SendExceptionRoute, error);
                if (returnTuple.Item2 == HttpStatusCode.OK)
                {
                    logger.Print("Your error has been sent to the server,thanks for your support.");
                }
                else
                {
                    string errorStr = JsonConvert.SerializeObject(error);
                    fileManager.WriteToFile(MainConfigs.ErrorsFile, errorStr);
                }

                string errorMessage = string.Format("Error! \n\nDo you want to continue?\n(if you click Yes you will continue with your work, if you click No the application will close)",

                e.Exception.Message + (e.Exception.InnerException != null ? "\n" +
                e.Exception.InnerException.Message : null));

                if (MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.No)
                {
                    Application.Current.Shutdown();
                }
            }

        }
    }
}
