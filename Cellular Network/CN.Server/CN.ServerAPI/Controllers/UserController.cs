using CN.Common.Configs;
using CN.Common.Contracts;
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
        IAccountsRepository accountsRepository { get; set; }
        public UserController()
        {
            accountsRepository = CnContainer.container.GetInstance<IAccountsRepository>();
        }
        public UserController(IAccountsRepository accountsRepository)
        {
            this.accountsRepository = accountsRepository;
        }

        [HttpPost]
        [Route(ApiConfigs.LoginRoute)]
        public User TryLogin([FromBody]UserLogin userLogin)
        {
            return accountsRepository.TryLogin(userLogin);
        }

    }
}
