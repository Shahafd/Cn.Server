using CN.Common.Configs;
using CN.Common.Contracts.IServices;
using CN.Common.Models.TempModels;
using CN.WebClient.Containers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CN.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error()
        {
            Exception e = Server.GetLastError();
            if (MainConfigs.CatchExceptions)
            {
                Error error = new Error(e, WebClientContainer.container.GetInstance<ISessionData>().GetLoggedInID());
                Tuple<object, HttpStatusCode> returnTuple = WebClientContainer.container.GetInstance<IHttpClient>().PostRequest(ApiConfigs.SendExceptionRoute, error);
                if (returnTuple.Item2 != HttpStatusCode.OK)
                {
                    string errorStr = JsonConvert.SerializeObject(error);
                    WebClientContainer.container.GetInstance<IFileManager>().WriteToFile(MainConfigs.ErrorsFile, errorStr);
                }

            }
        }
    }
}
