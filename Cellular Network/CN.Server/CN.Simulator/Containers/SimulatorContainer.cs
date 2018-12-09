using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.LoggersAndPoppers;
using CN.Common.Services;
using CN.Simulator.ViewModels;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Simulator.Containers
{
    public static class SimulatorContainer
    {
        public static Container container { get; set; }
        static SimulatorContainer()
        {
            if (container == null)
            {
                container = new Container();
                container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

                //ViewModels
                container.Register<ILoginViewModel, LoginViewModel>();
                container.Register<ISimulatorViewModel, SimulatorViewModel>();

                //Services
                container.Register<ILogger, MessageBoxPopper>(Lifestyle.Singleton);
                container.Register<IHttpClient, HttpClientSender>(Lifestyle.Singleton);
                container.Register<IInputsValidator, InputsValidator>(Lifestyle.Singleton);
                container.Register<ISessionData, SessionData>(Lifestyle.Singleton);
                container.Register<IFileManager, FileManager>(Lifestyle.Singleton);

            }
        }
    }
}
