using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Models.TempModels
{
    public class SimulatorAction
    {
        public string ClientId { get; set; }
        public int minDuration { get; set; }
        public int maxDuration { get; set; }
        public int numOfCalls { get; set; }
        public string destCall { get; set; }
        public string Line { get; set; }
        public string Type { get; set; }

        public SimulatorAction(string clientId, string Line, string type, string destCall, int numOfCalls, int minDuration, int maxDuration)
        {
            this.ClientId = clientId;
            this.Line = Line;
            this.Type = type;
            this.destCall = destCall;
            this.numOfCalls = numOfCalls;
            this.minDuration = minDuration;
            this.maxDuration = maxDuration;

        }

    }
}
