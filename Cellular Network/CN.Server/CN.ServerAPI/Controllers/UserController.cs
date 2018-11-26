using CN.Common.Configs;
using CN.Common.Contracts;
using CN.Common.Contracts.IManagers;
using CN.Common.Enums;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.Terminal.Containers;
using Newtonsoft.Json;
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
        ILinesManager linesManager { get; set; }
        public UserController()
        {
            accountsManager = CnContainer.container.GetInstance<IAccountsManager>();
            linesManager = CnContainer.container.GetInstance<ILinesManager>();
        }

        [HttpPost]
        [Route(ApiConfigs.LoginRoute)]
        public IHttpActionResult TryLogin([FromBody]UserLogin userLogin)
        {
            //a login on the clients
            User loggedIn = accountsManager.UserLogin(userLogin);
            if (loggedIn != null)
            {
                return Ok(loggedIn);
            }
            else
            {
                return Conflict();
            }
        }
        [HttpPost]
        [Route(ApiConfigs.UpdateExisitngClientRoute)]
        public IHttpActionResult UpdateClient([FromBody]Client client)
        {
            //updates an exisitng client's details
            return Ok(accountsManager.UpdateExisitngClient(client));
        }
        [HttpPost]
        [Route(ApiConfigs.IsClientIdExistsRoute)]
        public IHttpActionResult IsClientIdExists([FromBody]string clientId)
        {
            //checks if the client id exists
            return Ok(accountsManager.IsClientIdExists(clientId));
        }
        [HttpPost]
        [Route(ApiConfigs.AddClientRoute)]
        public IHttpActionResult AddClient([FromBody]Client client)
        {
            //updates an exisitng client's details
            return Ok(accountsManager.AddNewClient(client));
        }
        [HttpGet]
        [Route(ApiConfigs.GetAllClientsRoute)]
        public IHttpActionResult GetClients()
        {
            //returns the list of all the clients
            return Ok(accountsManager.GetAllClients());
        }
        [HttpPost]
        [Route(ApiConfigs.DeleteClientRoute)]
        public IHttpActionResult DeleteClient([FromBody]string id)
        {
            //deletes the client
            return Ok(accountsManager.DeleteClient(id));
        }

        [HttpPost]
        [Route(ApiConfigs.GetNewLine)]
        public IHttpActionResult GetNewLineNumber([FromBody]Client client)
        {
            //gets the lines of the client or a generated random new 1 if the client doesnt have any lines yet
            return Ok(linesManager.GetNewLine(client));
        }

        [HttpGet]
        [Route(ApiConfigs.GetDefaultPackagesRoute)]
        public IHttpActionResult GetDefaultPackages()
        {
            //returns the default packages
            return Ok(linesManager.GetDefaultPackages());
        }

        [HttpPost]
        [Route(ApiConfigs.GetPackageDetails)]
        public IHttpActionResult GetPackageDetails([FromBody]int packageId)
        {
            //returns the package details for this package
            return Ok(linesManager.GetPackageDetailsByPackageId(packageId));
        }

        [HttpPost]
        [Route(ApiConfigs.GetSelectedNumbersRoute)]
        public IHttpActionResult GetSelectedNumers([FromBody]int selectedNumbersId)
        {
            //returns the selectedNumbers that matches this id
            return Ok(linesManager.GetSelectedNumbersById(selectedNumbersId));
        }

        [HttpPost]
        [Route(ApiConfigs.GetClientLinesStrRoute)]
        public IHttpActionResult GetClientLines([FromBody]string clientId)
        {
            //returns a list of string represnting the client's lines
            List<Line> clientLines = linesManager.GetClientLinesByClientId(clientId);
            List<string> clientLinesStr = new List<string>();
            foreach (var item in clientLines)
            {
                clientLinesStr.Add(item.Number);
            }
            return Ok(clientLinesStr);
        }
        [HttpPost]
        public IHttpActionResult GetPackagesByLineId([FromBody] string lineId)
        {
            //returns the package that matches this line id
            return Ok(linesManager.GetPackageByLineId(lineId));
        }
        [HttpPost]
        public IHttpActionResult SendPackage([FromBody] LinePackObject linePackObj)
        {
            //gets the line and package object from the clients and add/updates it
            return Ok(linesManager.SendLinePackageObj(linePackObj));
        }
    }
}
