﻿using CN.Common.Models;
using CN.Common.Models.TempModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IManagers
{
    public interface  ISimulatorManager
    {
        Client GetClientByID(string clientID);
        List<Line> GetClientLines(string clientId);
       bool Simulate(SimulatorAction simulatorAction);
    }
}
