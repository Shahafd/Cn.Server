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

        //Clients
        public const string IsClientIdExistsRoute = "Api/user/IsClientidExists";
        public const string UpdateExisitngClientRoute = "Api/user/UpdateExistingClient";
        public const string AddClientRoute = "Api/user/AddClient";
        public const string GetAllClientsRoute = "Api/user/GetAllclients";
        public const string DeleteClientRoute = "Api/user/DeleteClient";


        //Lines And Packages
        public const string GetNewLine = "Api/user/GetNewLine";
        public const string GetClientLinesStrRoute = "Api/user/getClientStrLines"; 
        public const string GetDefaultPackagesRoute = "Api/user/GetDefaultPackages";
        public const string GetPackageDetails = "Api/user/GetPackageDetails";
        public const string GetSelectedNumbersRoute = "Api/user/GetSelectedNumbers";
        public const string GetPackageByLineRoute = "Api/user/GetPackageByLineId";

        //Simulator
        public const string GetClientLinesRoute = "Api/simulator/getClientLines";
        public const string GetClientByIdRoute = "Api/simulator/getClientByID";
        public const string SimulateRoute = "Api/simulator/Simulate";

        public const string SendLinePackageRoute = "Api/user/SendLinePackage";
    }
}
