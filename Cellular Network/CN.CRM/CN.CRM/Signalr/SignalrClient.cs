using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CN.CRM.Signalr
{
    public class SignalrClient : ISignalrClient
    {
        public HubConnection connection { get; set; }
        public IHubProxy hub { get; set; }
        public ILogger logger { get; set; }
        public SignalrClient(ILogger logger)
        {
            this.logger = logger;
            connection = new HubConnection(MainConfigs.Url);
            hub = connection.CreateHubProxy(MainConfigs.HubName);
            hub.On("Welcome", (string welcome) =>
{
    SayWelcome(welcome);
});
            try
            {
                //try connecting to the server
                connection.Start().Wait();
            }
            catch
            {
                //couldnt connect to the sever
                logger.Print("an error has occourd while trying to connect to the server");
            }
        }

        private void SayWelcome(string welcome)
        {
            logger.Print(welcome);
        }


        public User TrySendLogin(UserLogin userLogin)
        {
            //tries a login via the server
            Task<User> userLoginTask = Task.Run(async () =>
{
    return await hub.Invoke<User>("TryLogin", userLogin);
});
            return userLoginTask.Result;

        }


    }
}
