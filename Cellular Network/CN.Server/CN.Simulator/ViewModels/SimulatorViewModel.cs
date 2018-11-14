using CN.Common.Contracts.IServices;
using CN.Common.Contracts.IViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Simulator.ViewModels
{
    class SimulatorViewModel : ISimulatorViewModel
    {
        public IHttpClient httpClient { get; set; }
        public ObservableCollection<string> Lines { get; private set; }

        public SimulatorViewModel(IHttpClient httpClient)
        {
            this.httpClient = httpClient;

            Lines = new ObservableCollection<string>
         {
            "test 1",
            "test 2"
            };

        }

        //Methods:
        //get client  from DB(server) by client name (string)
        //get client lines from DB
        //
    }
}
