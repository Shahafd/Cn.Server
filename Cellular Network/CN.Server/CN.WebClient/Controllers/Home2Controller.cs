using CN.Common.Configs;
using CN.Common.Contracts.IServices;
using CN.Common.Models;
using CN.Common.Models.TempModels;
using CN.WebClient.Containers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CN.WebClient.Controllers
{
    public class Home2Controller : Controller
    {
        public IHttpClient httpClient { get; set; }
        public IInputsValidator inputsValidator { get; set; }
        public Home2Controller()
        {
            httpClient = WebClientContainer.container.GetInstance<IHttpClient>();
            inputsValidator = WebClientContainer.container.GetInstance<IInputsValidator>();
        }
        public PartialViewResult GetLineInfo(ClientWebModel client)
        {
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetLineDetailsRoute, client.SelectedLine);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JObject jobj = new JObject();
                jobj = (JObject)returnTuple.Item1;

            return PartialView("LineDetailsView",jobj.ToObject<LineDetails>());
            }
            else
            {
                ViewBag.ErrorMessage = returnTuple.Item2.ToString();
                return PartialView("LineDetailsView");
            }
        }
        public ActionResult ClientView(Client client)
        {
            Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.GetClientLinesStrRoute, client.ID);
            if (returnTuple.Item2 == HttpStatusCode.OK)
            {
                JArray jarr = new JArray();
                jarr = (JArray)returnTuple.Item1;
                ClientWebModel clientWebModel = new ClientWebModel($"{client.LastName} {client.FirstName}", jarr.ToObject<List<string>>());
            return View(clientWebModel);
            }
            else
            {
                ViewBag.ErrorMessage = returnTuple.Item2.ToString();
            }
            return View("Index");
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ClientLogin(ClientLogin clientLogin)
        {
            //a client tries to login
            List<string> info = new List<string>();
            List<string> errors = new List<string>();
            info.Add(inputsValidator.ValidateIDInput("ID", clientLogin.ID));
            info.Add(inputsValidator.ValidateYearOfBirthInput("Year Of Birth", clientLogin.YearOfBirth.ToString()));
            foreach (var item in info)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    errors.Add(item);
                }
            }
            for (int i = 0; i < errors.Count; i++)
            {
                if (i == 0)
                {
                    ViewBag.Message1 = errors[0];
                }
                if (i == 1)
                {
                    ViewBag.Message2 = errors[1];
                }
            }
            if (errors.Count == 0)
            {
                Tuple<object, HttpStatusCode> returnTuple = httpClient.PostRequest(ApiConfigs.ClientLoginRoute, clientLogin);
                if (returnTuple.Item2 == HttpStatusCode.OK)
                {
                    JObject jobj = new JObject();
                    jobj = (JObject)returnTuple.Item1;
                    return RedirectToAction("ClientView", jobj.ToObject<Client>());
                }
                else
                {
                    ViewBag.ErrorMessage = returnTuple.Item2;
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}