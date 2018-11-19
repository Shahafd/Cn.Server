using CN.Common.Contracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CN.Common.Contracts.IManagers;
using CN.Common.Models;

namespace CN.BL.Managers
{
    public class SimulatorManager : ISimulatorManager
    {
        public INetworkRepository networkRepository { get; set; }

        public SimulatorManager(INetworkRepository networkRepository)//network repo=DAL
        {
            this.networkRepository = networkRepository;
        }

        public Client GetClientByID(string clientID)
        {
            Client c = networkRepository.GetClientByID(clientID);
            Console.WriteLine("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$444");
            Console.WriteLine(c.ID);
            if (c != null)
            {
                return c;
            }
            else
            {
                return null;
            }

        }
        
        //methods that usr network.dosomething()
        
    }
}
