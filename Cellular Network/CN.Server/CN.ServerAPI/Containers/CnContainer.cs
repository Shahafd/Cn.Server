using CN.Common.Contracts;
using CN.Common.Contracts.Signalr;
using CN.DAL.Repositories;

using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Terminal.Containers
{
    public static class CnContainer
    {
        public static Container container { get; set; }
        static CnContainer()
        {
            if (container == null)
            {
                container = new Container();
                container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
                container.Register<IAccountsRepository, AccountsRepository>(Lifestyle.Singleton);

                container.Verify();
            }
        }


    }
}
