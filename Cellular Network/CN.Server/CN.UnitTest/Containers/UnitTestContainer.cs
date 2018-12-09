using CN.BL.Managers;
using CN.Common.Contracts.IManagers;
using CN.Common.Contracts.IRepositories;
using CN.DAL.Repositories;
using CN.UnitTest.MockRepositories;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.UnitTest.Containers
{
    class UnitTestContainer
    {
        public static Container container { get; set; }
        static UnitTestContainer()
        {
            if (container == null)
            {
                container = new Container();
                container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

                //Repositories
                container.Register<INetworkRepository, NetworkRepository>(Lifestyle.Singleton);

                //Managers
                container.Register<IAccountsManager, AccountsManager>(Lifestyle.Singleton);
                container.Register<ILinesManager, LinesManager>(Lifestyle.Singleton);
            }
        }
    }
}
