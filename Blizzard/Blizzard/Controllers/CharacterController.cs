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
using BusinessRules;
using System.Configuration;

namespace Blizzard.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private string FILENAME = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["LoginFileName"]);
        
        //
        // GET: /Account/Login
        public ActionResult Create()
        {
            CharacterModel cm = new CharacterModel();
            return View(cm);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Create(CharacterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CharacterRules cr = new CharacterRules(FILENAME, User.Identity.Name);
                    cr.AddCharacter(model.Name, model.Faction, model.Race, model.Class);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Login
        public ActionResult Edit(Guid Id)
        {
            CharacterRules cr = new CharacterRules(FILENAME, User.Identity.Name);
            CharacterModel cm = new CharacterModel();
            Character c = cr.GetCharacter(Id);
            cm.Name = c.Name;
            cm.Faction = c.Faction.ToString();
            cm.Race = c.Race.ToString();
            cm.Class = c.Class.ToString();
            return View(cm);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Edit(CharacterModel model, Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CharacterRules cr = new CharacterRules(FILENAME, User.Identity.Name);
                    cr.EditCharacter(id, model.Name, model.Faction, model.Race, model.Class);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }


            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}