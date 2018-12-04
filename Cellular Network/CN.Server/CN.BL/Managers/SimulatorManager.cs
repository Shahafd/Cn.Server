using CN.Common.Contracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CN.Common.Contracts.IManagers;
using CN.Common.Models;
using System.Diagnostics;
using System.Collections.ObjectModel;
using CN.Common.Models.TempModels;
using CN.Common.Enums;

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

            if (c != null)
            {
                return c;
            }
            else
            {
                return null;
            }

        }

        public List<Line> GetClientLines(string clientId)
        {
            return networkRepository.GetClientLines(clientId);
        }

        public bool Simulate(SimulatorAction simulatorAction)
        {
            if (simulatorAction.Type.Equals("Call"))
            {
                for (int i = 0; i < simulatorAction.numOfCalls; i++)
                {
                    double Duration = GetRandomNumber(simulatorAction.minDuration, simulatorAction.maxDuration);
                    Call call = new Call(simulatorAction.Line, Duration, 20.0, simulatorAction.destCall);
                    bool b = networkRepository.AddCall(call).Result;

                    if (!b)
                    {
                        return b;
                    }
                }
            }
            else
            {
                for (int i = 0; i < simulatorAction.numOfCalls; i++)
                {
                    double Duration = GetRandomNumber(simulatorAction.minDuration, simulatorAction.maxDuration);
                    SMS Sms = new SMS(simulatorAction.Line, 20.0, simulatorAction.destCall);
                    bool b = networkRepository.AddSms(Sms).Result;

                    if (!b)
                    {
                        return b;
                    }

                }
            }
            return true;


        }
        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }



    }
}
