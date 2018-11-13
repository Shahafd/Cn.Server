﻿using CN.BL.Services;
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.LoggersAndPoppers;
using CN.CRM.Signalr;
using CN.CRM.ViewModels;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.CRM.Containers
{
    public static class CrmContianer
    {
        public static Container container { get; set; }
        static CrmContianer()
        {
            if (container == null)
            {
                container = new Container();
                container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

                //ViewModels
                container.Register<ILoginViewModel, LoginViewModel>();
                container.Register<ICrmViewModel, CrmViewModel>();

                //Services
                container.Register<ILogger, MessageBoxPopper>(Lifestyle.Singleton);
                container.Register<IHttpClient, HttpClientSender>(Lifestyle.Singleton);

                container.Verify();
            }
        }


    }
}