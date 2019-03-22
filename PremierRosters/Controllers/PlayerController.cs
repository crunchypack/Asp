using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PremierRosters.Models;
using PremierRosters.Extentions;

namespace PremierRosters.Controllers
{
    public class PlayerController : Controller
    {
        // Create player form
        [HttpGet]
        [Authorize]
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
        
        // Get data to insert player to database
        [HttpPost]
        [Authorize]
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
        
        // Show all teams
        [HttpGet]
        public IActionResult Teams()
        {
            TeamMethods tm = new TeamMethods();
            List<TeamInfo> teamList = new List<TeamInfo>();
            teamList = tm.GetTeams(out string error);
            ViewBag.error = error;
            return View(teamList);
        }
        
        // Show all players
        [HttpGet]
        public IActionResult Players(string sortOrder)
        {
            PlayerMethods playerM = new PlayerMethods();
            TeamMethods tm = new TeamMethods();
            string sortBy;
            string error = "";
            Filter f = new Filter
            {
                teamInfo = tm.GetTeams(out string tError),
                sponsors = null,
                SpPl = null
            };
            // Handle click on links to sort 
            ViewBag.sort = sortOrder;
            ViewData["SortFirst"] = String.IsNullOrEmpty(sortOrder) ? "fNameSort_Desc" : "";
            ViewData["SortSur"] = sortOrder == "sName_Asc" ? "sName_Desc" : "sName_Asc";
            ViewData["SortPos"] = sortOrder == "Pos_Asc" ? "Pos_Desc" : "Pos_Asc";
            ViewData["SortTeam"] = sortOrder == "Team_Asc" ? "Team_Desc" : "Team_Asc";
            switch (sortOrder)
            {
                case "Pos_Asc":
                    sortBy = "pos";
                    f.playerInfo = playerM.GetPlayersInfo(sortBy, 0, out error);
                    //f.playerInfo = f.playerInfo.OrderByDescending(x => x.Position).ToList();
                    break;
                case "Pos_Desc":
                    sortBy = "pos";
                    f.playerInfo = playerM.GetPlayersInfo(sortBy, 1, out error);
                    break;
                case "Team_Asc":
                    sortBy = "team";
                    f.playerInfo = playerM.GetPlayersInfo(sortBy, 0, out error);
                    break;
                case "Team_Desc":
                    sortBy = "team";
                    f.playerInfo = playerM.GetPlayersInfo(sortBy, 1, out error);
                    break;
                case "sName_Asc":
                    sortBy = "last";
                    f.playerInfo = playerM.GetPlayersInfo(sortBy, 0, out error);
                    break;
                case "sName_Desc":
                    sortBy = "last";
                    f.playerInfo = playerM.GetPlayersInfo(sortBy, 1, out error);
                    break;
                case "fNameSort_Desc":
                    sortBy = "first";
                    f.playerInfo = playerM.GetPlayersInfo(sortBy, 1, out error);
                    break;
                default:
                    sortBy = "first";
                    f.playerInfo = playerM.GetPlayersInfo(sortBy, 0, out error);
                    break;
            }
            
            ViewBag.error ="player: "+ error + " team: "+ tError;
            return View(f);
        }
        
        // Handle search and filter
        [HttpPost]
        public IActionResult Players(int team,string search, int filter, string type)
        {
            
            PlayerMethods pm = new PlayerMethods();
            TeamMethods tm = new TeamMethods();
            Filter f = new Filter();
            string error;
            f.teamInfo = tm.GetTeams(out string tError);
            f.sponsors = pm.GetSponsors(out string sError);
            f.SpPl = null;
            if (String.IsNullOrEmpty(search) || team == -1)
            {
                if (team == -1) f.playerInfo = pm.GetPlayersInfo("first",0,out error);
                else f.playerInfo = pm.GetPlayersInfo(team, out error);
                
            }
            else
            {
                f.playerInfo = pm.SearchPlayersInfo(filter, type, search, out error);
            }
            f.playerInfo = f.playerInfo.OrderBy(x => x.Surname).ToList();
            ViewData["ID"] = team;
            ViewData["Filter"] = filter;
            ViewData["Search"] = search;
            
            return View(f);
        }
        
        // Get data for player to eventually delete
        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            PlayerMethods pm = new PlayerMethods();
            PlayerInfo pi = new PlayerInfo();
            pi = pm.GetPlayer(id, out string error);
            if (User.GetTeam() !=pi.TeamString && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Players");
            }
            ViewData["ID"] = id;
            ViewBag.error = error;
            return View(pi);
        }
        
        // Delete player from database
        [HttpPost]
        [Authorize]
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
        
        // Get data for player to edit
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            PlayerMethods pm = new PlayerMethods();
            TeamMethods tm = new TeamMethods();
            MainModell md = new MainModell
            {
                teamInfo = tm.GetTeams(out string tError),
                playerInfo = pm.GetPlayer(id, out string error)
            };
            if (User.GetTeam() != md.playerInfo.TeamString && !User.IsInRole("Admin"))
            {
                return RedirectToAction("Players");
            }
            ViewData["ID"] = id;
            ViewBag.error = error;
            return View(md);
        }
        
        // Update player information in the database
        [HttpPost]
        [Authorize]
        public IActionResult Edit(MainModell main)
        {
            PlayerMethods pm = new PlayerMethods();
            TeamMethods tm = new TeamMethods();
            main.teamInfo = tm.GetTeams(out string tError);
            int i = pm.UpdatePlayer(main.playerInfo, out string error);
            ViewBag.error = error + ".\n Player Name: " + main.playerInfo.FirstName+ "\nPlayerID: " + main.playerInfo.ID;

            if (i == 1) return RedirectToAction("Players");
            else
                return View(main);
        }
        
        // Get sponsors and the players they sponsor
        public IActionResult Sponsors()
        {
            PlayerMethods pm = new PlayerMethods();
            List<SponsorInfo> si = new List<SponsorInfo>();
            si = pm.GetSponsors(out string error);

            foreach(var sponsor in si)
            {
                sponsor.Players = pm.GetSponsorsPlayers(sponsor.ID, out string error2);
            }
            ViewBag.error = error;

            return View(si);
        }
        
        // Start to add sponsor to player - picks sponsor
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddSponsor()
        {
            PlayerMethods pm = new PlayerMethods();
            Filter filter = new Filter
            {
                playerInfo = pm.GetPlayersInfo("last",0,out string pError),
                sponsors = pm.GetSponsors(out string sError),
                teamInfo = null,
                SpPl = new PlayerSponsorBy()
            };
           // filter.SpPl.SponsoredBy = pm.GetRelations(out string reError);
            ViewBag.error = pError;
            return View(filter);
        }
        
        // With given sponsor check for already sponsored players
        // Remove players from list - send list to view
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddPlayerToSp(int SponsorID)
        {
            PlayerMethods pm = new PlayerMethods();
            PlayerSponsorBy player = new PlayerSponsorBy();
            List<PlayerInfo> players = pm.GetPlayersInfo("last",0,out string pError);
            Filter filter = new Filter
            {
                sponsors = pm.GetSponsors(out string sError),
                teamInfo = null,
                SpPl = new PlayerSponsorBy()
            };
            filter.SpPl.SponsoredBy = pm.GetRelations(out string reError);
            var result = filter.SpPl.SponsoredBy.Where(kv => kv.Key == SponsorID);
            foreach (PlayerInfo p in players.ToList())
            {
                foreach (KeyValuePair<int, int> kvp in result)
                {
                    if (kvp.Value == p.ID)
                    {
                        var remove = players.SingleOrDefault(x => x.ID == p.ID);
                        if (remove != null) players.Remove(remove);
                    }

                }
            }
            filter.playerInfo = players;
            ViewBag.error = pError + " "+ reError;
            ViewData["ID"] = SponsorID;
            return View(filter);
        }
        
        // Get sponsor and player to create relation in database
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public IActionResult CreateRelation(Filter filter)
        {
            filter.playerInfo = null;
            filter.sponsors = null;
            PlayerMethods pm = new PlayerMethods();
            int i = pm.InsertSpRelation(filter.SpPl, out string error);
            ViewBag.error = error;
            ViewBag.player = filter.SpPl.Player;
            ViewBag.sponsor = filter.SpPl.Sponsor;
            if (i == 1) return RedirectToAction("Sponsors");
            else
                return View();
        }
        // Delete player - sponsor relation
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSp(int id)
        {
            PlayerMethods pm = new PlayerMethods();
            SponsorInfo si = new SponsorInfo();
            si = pm.GetSponsor(id,out string error);
            si.Players = pm.GetSponsorsPlayers(id, out string error2);
            
            ViewBag.error = error;

            return View(si);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteSp(int playerID, int sponsorID)
        {
            PlayerMethods pm = new PlayerMethods();
            int i = pm.RemoveSpRelation(playerID, sponsorID, out string error3);

            SponsorInfo si = new SponsorInfo();


            si = pm.GetSponsor(sponsorID, out string error);
            si.Players = pm.GetSponsorsPlayers(sponsorID, out string error2);
            ViewBag.error = error + "\n"+ error2 + "\n" + error3;
            ViewBag.Data = "Player ID: " + playerID + "\nSponsor ID: " + sponsorID;
            if (i == 1) return RedirectToAction("Sponsors");
            else return View(si);
        }
        // Get player details
        [HttpGet]
        public IActionResult Details(int id)
        {
            PlayerMethods pm = new PlayerMethods();
            PlayerInfo pi = new PlayerInfo();
            pi = pm.GetPlayer(id, out string error);

            return View(pi);
        }

    }
}