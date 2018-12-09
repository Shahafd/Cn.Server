using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.LoggersAndPoppers;
using CN.Common.Services;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CN.WebClient.Containers
{
    public static class WebClientContainer
    {
        public static Container container { get; set; }
        static WebClientContainer()
        {
            if (container == null)
            {
                container = new Container();
                container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
                                           
                container.Register<IHttpClient, HttpClientSender>(Lifestyle.Singleton);
                container.Register<IInputsValidator, InputsValidator>(Lifestyle.Singleton);
                container.Register<ILogger, DebugLogger>(Lifestyle.Singleton);
                container.Register<IFileManager, FileManager>(Lifestyle.Singleton);
                container.Register<ISessionData, SessionData>(Lifestyle.Singleton);
                container.Verify();
            }
        }
    }
}