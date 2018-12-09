
using CN.Common.Contracts;
using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using CN.Common.Enums;
using CN.Common.LoggersAndPoppers;
using CN.Common.Models;
using CN.Common.Services;

using CN.CRM.ViewModels;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                container.Register<ICrmViewModel, CrmViewModel>(Lifestyle.Singleton);
                container.Register<IAddEditClientViewModel, AddEditClientViewModel>();
                container.Register<IAddEditLineViewModel, AddEditLineViewModel>();
                container.Register<IBillViewModel, BillViewModel>();
                container.Register<IBillDatePickerViewModel, BillDatePickerViewModel>();

                //Services
                container.Register<ILogger, MessageBoxPopper>(Lifestyle.Singleton);
                container.Register<IHttpClient, HttpClientSender>(Lifestyle.Singleton);
                container.Register<IInputsValidator, InputsValidator>(Lifestyle.Singleton);
                container.Register<ISessionData, SessionData>(Lifestyle.Singleton);
                container.Register<IFileManager, FileManager>(Lifestyle.Singleton);


                container.Verify();
            }
        }


    }
}
