using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Configs
{
    public static class ApiConfigs
    {
        public const string LoginRoute = "Api/user/Login";
        public const string UpdateExistingClientRoute= "Api/user/UpdateExistingClient";
        public const string AddClientRoute = "Api/user/AddClient";

        public const string GetClientByIdRoute = "Api/simulator/getClientByID";
        public const string GetClientLinesRoute = "Api/simulator/getClientLines";
    }
}
