using CN.Common.Configs;
using CN.Common.Contracts.IManagers;
using CN.Common.Enums;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.Terminal.Containers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public IHttpActionResult getClientById([FromBody]string clientId)
        {
            return Ok(simulatorManager.GetClientByID(clientId));

        }

        [HttpPost]
        [Route(ApiConfigs.GetClientLinesRoute)]
        public IHttpActionResult GetClientLines([FromBody]string clientId)
        {
            return Ok(simulatorManager.GetClientLines(clientId));
        }

        [HttpPost]
        [Route(ApiConfigs.SimulateRoute)]
        public IHttpActionResult Simulate([FromBody] SimulatorAction simulatorAction)
        {
            string s = simulatorManager.Simulate(simulatorAction).Result;
            if (s.Equals(RequestStatusEnum.Success))//chek not work
            {
                return Ok(s);
            }
            return null;
        }

    }
}
