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
        public IActionResult Players(string sortOrder)
        {
            PlayerMethods playerM = new PlayerMethods();
            TeamMethods tm = new TeamMethods();
            Filter f = new Filter
            {
                playerInfo = playerM.GetPlayersInfo(out string error),
                teamInfo = tm.GetTeams(out string tError)
            };
            ViewBag.sort = sortOrder;
            ViewData["SortFirst"] = String.IsNullOrEmpty(sortOrder) ? "fNameSort_Desc" : "";
            ViewData["SortSur"] = sortOrder == "sName_Asc" ? "sName_Desc" : "sName_Asc";
            ViewData["SortPos"] = sortOrder == "Pos_Asc" ? "Pos_Desc" : "Pos_Asc";
            ViewData["SortTeam"] = sortOrder == "Team_Asc" ? "Team_Desc" : "Team_Asc";
            switch (sortOrder)
            {
                case "Pos_Asc":
                    f.playerInfo = f.playerInfo.OrderBy(x => x.Position).ToList();
                    break;
                case "Pos_Desc":
                    f.playerInfo = f.playerInfo.OrderByDescending(x => x.Position).ToList();
                    break;
                case "Team_Asc":
                    f.playerInfo = f.playerInfo.OrderBy(x => x.TeamString).ToList();
                    break;
                case "Team_Desc":
                    f.playerInfo = f.playerInfo.OrderByDescending(x => x.TeamString).ToList();
                    break;
                case "sName_Asc":
                    f.playerInfo = f.playerInfo.OrderBy(x => x.Surname).ToList();
                    break;
                case "sName_Desc":
                    f.playerInfo = f.playerInfo.OrderByDescending(x => x.Surname).ToList();
                    break;
                case "fNameSort_Desc":
                    f.playerInfo = f.playerInfo.OrderByDescending(x => x.FirstName).ToList();
                    break;
                default:
                    f.playerInfo = f.playerInfo.OrderBy(x => x.FirstName).ToList();
                    break;
            }
           // f.playerInfo = f.playerInfo.OrderBy(x => x.Surname).ToList();
            ViewBag.error ="player: "+ error + " team: "+ tError;
            return View(f);
        }
        [HttpPost]
        public IActionResult Players(int team,string search, int filter, string type,string sortOrder)
        {
            
            PlayerMethods pm = new PlayerMethods();
            TeamMethods tm = new TeamMethods();
            Filter f = new Filter();
            string error;
            f.teamInfo = tm.GetTeams(out string tError);
            if (String.IsNullOrEmpty(search) || team == -1)
            {
                if (team == -1) f.playerInfo = pm.GetPlayersInfo(out error);
                else f.playerInfo = pm.GetPlayersInfo(team, out error);
                
            }
            else
            {
                f.playerInfo = pm.SearchPlayersInfo(filter, type, search, out error);
            }
            f.playerInfo = f.playerInfo.OrderBy(x => x.Surname).ToList();
            ViewData["ID"] = team;
            ViewData["Filter"] = filter;
            
            //ViewData["Stuff"] = "Stuff: " + search + " " + type + " " + filter;
            //ViewBag.error = "player: " + error + " team: " + tError + " ID: " + team;

            
            return View(f);
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
            ViewBag.error = error;
            if (i == 1) return RedirectToAction("Players");
            else
            return View(pi);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PlayerMethods pm = new PlayerMethods();
            TeamMethods tm = new TeamMethods();
            MainModell md = new MainModell
            {
                teamInfo = tm.GetTeams(out string tError),
                playerInfo = pm.GetPlayer(id, out string error)
            };
            
            ViewData["ID"] = id;
            ViewBag.error = error;
            return View(md);
        }

    }
}