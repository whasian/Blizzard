using Blizzard.Models;
using Services;
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
            PlayerService ps = new PlayerService(FILENAME);
            pm.Player = ps.GetPlayer(User.Identity.Name);
            return View(pm);
        }
    }
}