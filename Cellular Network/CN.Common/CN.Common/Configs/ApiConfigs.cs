﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Configs
{
    public static class ApiConfigs
    {
        public const string LoginRoute = "Api/user/Login";
        public const string ManagerLoginRoute = "Api/user/managerLogin";

        //Clients
        public const string IsClientIdExistsRoute = "Api/user/IsClientidExists";
        public const string UpdateExisitngClientRoute = "Api/user/UpdateExistingClient";
        public const string AddClientRoute = "Api/user/AddClient";
        public const string GetAllClientsRoute = "Api/user/GetAllclients";
        public const string DeleteClientRoute = "Api/user/DeleteClient";
        public const string ClientSearchRoute = "Api/user/clientSearch";
        public const string ClientLoginRoute = "Api/user/clientLogin";

        //Managers
        public const string GetValueClientsRoute = "Api/user/getValueClients";
        public const string GetMostCallingRoute = "Api/user/GetMostCallingClients";
        public const string GetBestSellersRoute = "api/user/getBestSellers";

        //Lines And Packages
        public const string GetNewLine = "Api/user/GetNewLine";
        public const string GetClientLinesStrRoute = "Api/user/getClientStrLines";
        public const string GetDefaultPackagesRoute = "Api/user/GetDefaultPackages";
        public const string GetPackageDetails = "Api/user/GetPackageDetails";
        public const string GetSelectedNumbersRoute = "Api/user/GetSelectedNumbers";
        public const string GetPackageByLineRoute = "Api/user/GetPackageByLineId";
        public const string SendLinePackageRoute = "Api/user/SendLinePackage";
        public const string DeleteLineRoute = "Api/user/DeleteLine";
        public const string GetLineStatusRoute = "Api/user/GetLineStatus";
        public const string SendLineStatusRoute = "Api/user/SendLineStatus";
        public const string GetLineDetailsRoute = "Api/user/GeLineDetails";
        public const string GetSpecficLinesBillRoute = "Api/user/GetSpecificLinesBill";
        public const string CheckIfLineExistedRoute = "Api/user/checkiflinesexistedRoute";



        //Simulator
        public const string GetClientLinesRoute = "Api/simulator/getClientLines";
        public const string GetClientByIdRoute = "Api/simulator/getClientByID";
        public const string SimulateRoute = "Api/simulator/Simulate";

        //EXCEPTIONS
        public const string SendExceptionRoute = "Api/user/SendException";
        public const string SendExceptionsListRoute = "Api/user/SendExceptionsList";

      
    }
}
