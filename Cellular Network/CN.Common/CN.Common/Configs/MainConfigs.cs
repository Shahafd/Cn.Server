using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Configs
{
    public static class MainConfigs
    {
        public const string ConnectionString = "CellularNetworkConnectionString";
        public const string Url = "http://localhost:65161/";
        public const string HubName = "CnHub";

        public static List<int> Months = new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public static List<int> Years = new List<int> { 2014, 2015, 2016, 2017, 2018, 2019 };
    }
}
