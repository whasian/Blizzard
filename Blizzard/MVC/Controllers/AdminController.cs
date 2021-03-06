﻿using System;
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
using Core;
using System.Web.Hosting;
using System.Text;
using Newtonsoft.Json;
using Services;
using System.Configuration;

namespace Blizzard.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private string FILENAME = HostingEnvironment.MapPath(ConfigurationManager.AppSettings["LoginFileName"]);

        public ActionResult View()
        {
            // check access
            if (VerifyAccess())
            {
                return RedirectToAction("Index", "Home");
            }
            PlayerService ps = new PlayerService(FILENAME);
            PlayersModel pm = new PlayersModel();
            pm.Players = ps.GetPlayer();

            return View(pm);
        }

        public ActionResult PlayerEdit(string id)
        {
            // check access
            if (VerifyAccess())
            {
                return RedirectToAction("Index", "Home");
            }
            PlayerService ps = new PlayerService(FILENAME);
            PlayerModel pm = new PlayerModel();
            Player p = ps.GetPlayer(id);

            pm.Player = p;
            return View(pm);
        }

        [HttpPost]
        public ActionResult PlayerEdit(PlayerModel model, string id)
        {
            // check access
            if (VerifyAccess())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    PlayerService ps = new PlayerService(FILENAME);
                    ps.EditPlayer(id, model.Player.IsAdmin);
                    if (User.Identity.Name.Equals(id, StringComparison.OrdinalIgnoreCase))
                    {
                        Session["OverRideIsAdmin"] = model.Player.IsAdmin;
                    }
                    return RedirectToAction("View", "Admin");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }

            return View(model);
        }

        public ActionResult CharacterEdit(Guid id, string userName)
        {
            // check access
            if (VerifyAccess())
            {
                return RedirectToAction("Index", "Home");
            }
            CharacterService cr = new CharacterService(FILENAME, userName);
            CharacterModel cm = new CharacterModel();
            Character c = cr.GetCharacter(id);
            cm.Name = c.Name;
            cm.Faction = c.Faction.ToString();
            cm.Race = c.Race.ToString();
            cm.Class = c.Class.ToString();
            cm.Level = c.Level;
            cm.Active = c.Active;
            return View(cm);
        }

        [HttpPost]
        public ActionResult CharacterEdit(CharacterModel model, Guid id, string userName)
        {
            // check access
            if (VerifyAccess())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                 try
                 {
                     CharacterService cs = new CharacterService(FILENAME, userName);
                     cs.EditCharacter(id, model.Name, model.Faction, model.Race, model.Class, model.Level, model.Active);
                     return RedirectToAction("PlayerEdit", "Admin", new { id = userName });
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("", e.Message);
                 }
            }

            return View(model);
        }

        private bool VerifyAccess()
        {
            return (bool?)Session["OverRideIsAdmin"] == false || !((CustomPrincipal)User).IsAdmin;
        }
    }
}