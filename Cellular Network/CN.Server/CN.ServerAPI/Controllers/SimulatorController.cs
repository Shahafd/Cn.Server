using CN.Common.Configs;
using CN.Common.Contracts.IManagers;
using CN.Common.Models;
using CN.Terminal.Containers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CN.ServerAPI.Controllers
{
    public class SimulatorController : ApiController
    {
        public ISimulatorManager simulatorManager { get; set; }

        public SimulatorController()
        {
            simulatorManager = CnContainer.container.GetInstance<ISimulatorManager>();
        }

        [HttpPost]
        [Route(ApiConfigs.GetClientByIdRoute)]
        public Client getClientById(string clientId)
        {
            var x = simulatorManager.GetClientByID(clientId);
            return x;
        }

    }
}
