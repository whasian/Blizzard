using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Blizzard.Models;
using System.Web.Security;
using Business;
using System.Web.Hosting;
using System.Text;
using Newtonsoft.Json;
using Services;
using System.Configuration;

namespace Blizzard.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private string FILENAME = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["LoginFileName"]);
        
        public ActionResult Create()
        {
            PlayerService ps = new PlayerService(FILENAME);
            var p = ps.GetPlayer(User.Identity.Name);

            if (p.Characters.Exists(x => x.Faction == CharacterFaction.Horde && x.Active == true))
            {
                ViewBag.PlayerFaction = "Horde";
            }
            else if (p.Characters.Exists(x => x.Faction == CharacterFaction.Alliance && x.Active == true))
            {
                ViewBag.PlayerFaction = "Alliance";
            }

            CharacterAddModel cm = new CharacterAddModel();
            return View(cm);
        }

        [HttpPost]
        public ActionResult Create(CharacterAddModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CharacterService cs = new CharacterService(FILENAME, User.Identity.Name);
                    cs.AddCharacter(model.Name, model.Faction, model.Race, model.Class);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);
        }

        public ActionResult View(Guid Id)
        {
            CharacterService cs = new CharacterService(FILENAME, User.Identity.Name);
            CharacterModel cm = new CharacterModel();
            Character c = cs.GetCharacter(Id);
            cm.Name = c.Name;
            cm.Faction = c.Faction.ToString();
            cm.Race = c.Race.ToString();
            cm.Class = c.Class.ToString();
            cm.Level = c.Level;
            cm.Active = c.Active;

            PlayerService ps = new PlayerService(FILENAME);
            var p = ps.GetPlayer(User.Identity.Name);

            if (p.Characters.Exists(x => x.Faction == CharacterFaction.Horde && x.Active == true))
            {
                ViewBag.PlayerFaction = "Horde";
            }
            else if (p.Characters.Exists(x => x.Faction == CharacterFaction.Alliance && x.Active == true))
            {
                ViewBag.PlayerFaction = "Alliance";
            }

            return View(cm);
        }

        public ActionResult ActiveToggle(Guid Id)
        {
            CharacterService cs = new CharacterService(FILENAME, User.Identity.Name);
            try
            {
                Character c = cs.GetCharacter(Id);
                cs.EditCharacter(c.Id, c.Name, c.Faction.ToString(), c.Race.ToString(), c.Class.ToString(), c.Level, !c.Active);
            }
            catch(Exception e)
            {
            }

            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public ActionResult Edit(CharacterModel model, Guid id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            CharacterRules cr = new CharacterRules(FILENAME, User.Identity.Name);
        //            cr.EditCharacter(id, model.Name, model.Faction, model.Race, model.Class);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        catch (Exception e)
        //        {
        //            ModelState.AddModelError("", e.Message);
        //        }
        //    }

        //    return View(model);
        //}
    }
}