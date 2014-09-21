using Blizzard.Models;
using BusinessRules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Blizzard.Controllers
{
    public class HomeController : Controller
    {
        private string FILENAME = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["LoginFileName"]);

        [Authorize]
        public ActionResult Index()
        {
            PlayerModel pm = new PlayerModel();
            PlayerRules pr = new PlayerRules(FILENAME);
            pm.Player = pr.GetPlayer(User.Identity.Name);
            return View(pm);
        }
    }
}