using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class TeamMethods
    {
        public TeamMethods() { }

        public List<TeamInfo> GetTeams(out string error)
        {
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };

            // Query for getting teams
            String sqlQuery = "SELECT Te_Name AS Name,Te_ID AS ID, He_Surname AS Coach FROM Tbl_Team INNER JOIN Tbl_Headcoach ON Te_Headcoach = He_ID";
            SqlCommand sCommand = new SqlCommand(sqlQuery, sConnection);

            SqlDataReader read = null;
            List<TeamInfo> teamlist = new List<TeamInfo>();

            error = "";
            try
            {
                sConnection.Open();
                read = sCommand.ExecuteReader();

                while (read.Read())
                {
                    TeamInfo team = new TeamInfo();
                    team.Name = read["Name"].ToString();
                    team.Headcoach = read["Coach"].ToString();
                    team.ID = Convert.ToInt32(read["ID"]);
                    teamlist.Add(team);
                }
                read.Close();
                return teamlist;
            }catch(Exception e)
            {
                TeamInfo team = new TeamInfo();
                team.Name = "Error";
                team.Headcoach = "Coach";
                team.ID = 1;
                teamlist.Add(team);
                error = e.Message;
                return teamlist;
            }
            finally
            {
                sConnection.Close();
            }
        }
    }
}
