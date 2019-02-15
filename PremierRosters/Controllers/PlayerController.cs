using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PremierRosters.Models;

namespace PremierRosters.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult InsertPlayer()
        {
            TeamMethods tm = new TeamMethods();
            MainModell main = new MainModell
            {
                teamInfo = tm.GetTeams(out string error)
            };
            
            ViewBag.error = error;

            return View(main);
        }
        [HttpPost]
        public IActionResult InsertPlayer(MainModell main)
        {
            PlayerInfo player = new PlayerInfo();
            player = main.playerInfo;
            PlayerMethods playerM = new PlayerMethods();
            int i=  playerM.InsertPlayer(player, out string error);
            TeamMethods tm = new TeamMethods();
            main.teamInfo = tm.GetTeams(out string error1);
            ViewData["Player"] = player.Team;
            ViewBag.error = error;
            if(i == 1)  return RedirectToAction("Players"); 
            else
            return View(main);
        }
        [HttpGet]
        public IActionResult Teams()
        {
            TeamMethods tm = new TeamMethods();
            List<TeamInfo> teamList = new List<TeamInfo>();
            teamList = tm.GetTeams(out string error);
            ViewBag.error = error;
            return View(teamList);
        }
        [HttpGet]
        public IActionResult Players()
        {
            PlayerMethods playerM = new PlayerMethods();
            List<PlayerInfo> players = new List<PlayerInfo>();
            players = playerM.GetPlayerInfo(out string error);
            ViewBag.error = error;
            return View(players);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            PlayerMethods pm = new PlayerMethods();
            PlayerInfo pi = new PlayerInfo();
            pi = pm.GetPlayer(id, out string error);
            ViewData["ID"] = id;
            ViewBag.error = error;
            return View(pi);
        }
        
        [HttpPost]
        public IActionResult Delete(PlayerInfo pi)
        {
            PlayerMethods pm = new PlayerMethods();
            int id = pi.ID;
            int i = pm.DeletePlayer(id, out string error);
            ViewData["ID"] = id;
            ViewBag.error = error;
            if (i == 1) return RedirectToAction("Players");
            else
            return View(pi);
        }


    }
}