using CN.DAL.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CN.ServerAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CnContext db = new CnContext();
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
