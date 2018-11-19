using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IManagers;
using CN.Common.Enums;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.Terminal.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CN.ServerAPI.Controllers
{
    public class UserController : ApiController
    {
        IAccountsManager accountsManager { get; set; }
        public UserController()
        {
            accountsManager = CnContainer.container.GetInstance<IAccountsManager>();
        }
      
        [HttpPost]
        [Route(ApiConfigs.LoginRoute)]
        public User TryLogin([FromBody]UserLogin userLogin)
        {
            return accountsManager.UserLogin(userLogin).Item1;
        }

    }
}
