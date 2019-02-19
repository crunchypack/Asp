using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PremierRosters.Models
{
    public class PlayerMethods
    {
        // Empty constructor
        public PlayerMethods() { }
        // Add player to the database
        public int InsertPlayer(PlayerInfo pi, out string error)
        {
            // SQL Connection created
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True";

            // Query for inserting Player
            String sqlQuery = "INSERT INTO Tbl_Player(Pl_First_name, Pl_Surname, Pl_Jersey, Pl_Position, Pl_Birthyear, Pl_Team) VALUES(@first,@last,@jersey,@pos,@birth,@team)";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);

            sqlCommand.Parameters.Add("first", SqlDbType.NVarChar, 20).Value = pi.FirstName;
            sqlCommand.Parameters.Add("last", SqlDbType.NVarChar, 20).Value = pi.Surname;
            sqlCommand.Parameters.Add("jersey", SqlDbType.Int).Value = pi.Jersey;
            sqlCommand.Parameters.Add("pos", SqlDbType.NVarChar, 50).Value = pi.Position;
            sqlCommand.Parameters.Add("birth", SqlDbType.Int).Value = pi.BirthYear;
            sqlCommand.Parameters.Add("team", SqlDbType.Int).Value = pi.Team;

            try
            {
                sqlConnection.Open();
                int i = 0;
                i = sqlCommand.ExecuteNonQuery();
                if(i == 1) { error = ""; }
                else { error = "Player not created"; }
                return (i);
            }
            catch(Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        
        // Get all players
        public List<PlayerInfo> GetPlayersInfo(out string error)
        {
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };

            // Query for getting teams
            String sqlQuery = "SELECT Pl_ID AS ID, Pl_First_name AS [First name], Pl_Surname AS Surname, Pl_Position AS Position, Te_Name  AS Team From Tbl_Player INNER JOIN Tbl_Team ON Te_ID = Pl_Team";
            SqlCommand sCommand = new SqlCommand(sqlQuery, sConnection);

            SqlDataReader read = null;
            List<PlayerInfo> playerList= new List<PlayerInfo>();

            error = "";
            try
            {
                sConnection.Open();
                read = sCommand.ExecuteReader();

                while (read.Read())
                {
                    PlayerInfo player = new PlayerInfo();
                    player.FirstName = read["First Name"].ToString();
                    player.Surname = read["Surname"].ToString();
                    player.Position = read["Position"].ToString();
                    player.TeamString = read["Team"].ToString();
                    player.ID = Convert.ToInt32(read["ID"]);
                    playerList.Add(player);
                }
                read.Close();
                return playerList;
            }
            catch (Exception e)
            {
                PlayerInfo player = new PlayerInfo();
                player.FirstName = "Error";
                playerList.Add(player);
                error = e.Message;
                return playerList;
            }
            finally
            {
                sConnection.Close();
            }
        }

        // Get all players from one team
        public List<PlayerInfo> GetPlayersInfo(int id, out string error)
        {
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };

            // Query for getting teams
            String sqlQuery = "SELECT Pl_ID AS ID, Pl_First_name AS [First name], Pl_Surname AS Surname, Pl_Position AS Position, Te_Name  AS Team From Tbl_Player INNER JOIN Tbl_Team ON Te_ID = Pl_Team WHERE Pl_Team = @Id";
            SqlCommand sCommand = new SqlCommand(sqlQuery, sConnection);

            sCommand.Parameters.Add("Id", SqlDbType.Int).Value = id;
            SqlDataReader read = null;
            List<PlayerInfo> playerList = new List<PlayerInfo>();

            error = "";
            try
            {
                sConnection.Open();
                read = sCommand.ExecuteReader();

                while (read.Read())
                {
                    PlayerInfo player = new PlayerInfo();
                    player.FirstName = read["First Name"].ToString();
                    player.Surname = read["Surname"].ToString();
                    player.Position = read["Position"].ToString();
                    player.TeamString = read["Team"].ToString();
                    player.ID = Convert.ToInt32(read["ID"]);
                    playerList.Add(player);
                }
                read.Close();
                return playerList;
            }
            catch (Exception e)
            {
                PlayerInfo player = new PlayerInfo();
                player.FirstName = "Error";
                playerList.Add(player);
                error = e.Message;
                return playerList;
            }
            finally
            {
                sConnection.Close();
            }
        }

        //Delete player from database
        public int DeletePlayer(int id, out string error)
        {
            // SQL Connection created
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True";

            // Query for inserting Player
            String sqlQuery = "DELETE FROM Tbl_Player WHERE Pl_ID = @id";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);

            sqlCommand.Parameters.Add("id", SqlDbType.Int).Value = id;

            try
            {
                sqlConnection.Open();
                int i = 0;
                i = sqlCommand.ExecuteNonQuery();
                if (i == 1) { error = ""; }
                else { error = "Player not Deleted"; }
                return (i);
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // Get one player with id
        public PlayerInfo GetPlayer(int id, out string error)
        {
            int newID = id;
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };

            // Query for getting teams
            String sqlQuery = "SELECT PL_ID AS ID,Pl_First_name AS [First Name], Pl_Surname AS [Surname], Pl_Position AS Position, Pl_Jersey AS Jersey, Pl_Birthyear AS Birthyear,Te_Name  AS Team, Pl_Team AS TeamID From Tbl_Player INNER JOIN Tbl_Team ON Te_ID = Pl_Team"
                +" WHERE Pl_ID = @Id; ";
            SqlCommand sCommand = new SqlCommand(sqlQuery, sConnection);

            sCommand.Parameters.Add("Id", SqlDbType.Int).Value = newID;
            SqlDataReader read = null;
         

            error = "";
            try
            {
                sConnection.Open();
                read = sCommand.ExecuteReader();
                PlayerInfo player = new PlayerInfo();
                while (read.Read())
                {
                    
                    player.FirstName = read["First Name"].ToString();
                    player.Surname = read["Surname"].ToString();
                    player.Position = read["Position"].ToString();
                    player.Jersey = Convert.ToInt32(read["Jersey"]);
                    player.BirthYear = Convert.ToInt32(read["Birthyear"]);
                    player.TeamString = read["Team"].ToString();
                    player.Team = Convert.ToInt32(read["TeamID"]);
                    player.ID = Convert.ToInt32(read["ID"]);
                }
                
                    
                
                read.Close();
                return player;
            }
            catch (Exception e)
            {
                PlayerInfo player = new PlayerInfo();
                player.FirstName = "Error";
                
                error = e.Message +" "+  id;
                return player;
            }
            finally
            {
                sConnection.Close();
            }
        }

        // Get all players that match search query
        public List<PlayerInfo> SearchPlayersInfo(int id, string type, string search, out string error)
        {
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };
            String sqlQuery;
            SqlCommand sCommand;
            search = "%" + search + "%";
            if (id > 0)
            {
                if(type == "Name")
                {
                    sqlQuery = "SELECT Pl_ID AS ID, Pl_First_name AS [First name], Pl_Surname AS Surname, Pl_Position AS Position, "+
                        "Te_Name  AS Team From Tbl_Player INNER JOIN Tbl_Team ON Te_ID = Pl_Team WHERE Pl_Team = @Id AND Pl_Surname LIKE @Search;";
                    
                }
                else
                {
                    sqlQuery = "SELECT Pl_ID AS ID, Pl_First_name AS [First name], Pl_Surname AS Surname, Pl_Position AS Position, "+
                        "Te_Name  AS Team From Tbl_Player INNER JOIN Tbl_Team ON Te_ID = Pl_Team WHERE Pl_Team = @Id AND Pl_Position LIKE @Search;";
                    

                }
                sCommand = new SqlCommand(sqlQuery, sConnection);
                
                sCommand.Parameters.Add("Id", SqlDbType.Int).Value = id;
                sCommand.Parameters.Add("Search", SqlDbType.NVarChar, 50).Value = search;
            }
            else
            {
                if(type == "Name")
                {
                    sqlQuery = "SELECT Pl_ID AS ID, Pl_First_name AS [First name], Pl_Surname AS Surname, Pl_Position AS Position,"+
                        "Te_Name  AS Team From Tbl_Player INNER JOIN Tbl_Team ON Te_ID = Pl_Team WHERE Pl_Surname LIKE @search;";
                }
                else
                {
                    sqlQuery = "SELECT Pl_ID AS ID, Pl_First_name AS [First name], Pl_Surname AS Surname, Pl_Position AS Position, "+
                        "Te_Name  AS Team From Tbl_Player INNER JOIN Tbl_Team ON Te_ID = Pl_Team WHERE Pl_Position LIKE @search;";
                }
                sCommand = new SqlCommand(sqlQuery, sConnection);
                sCommand.Parameters.Add("search", SqlDbType.NVarChar, 50).Value = search;


            }
            
            SqlDataReader read = null;
            List<PlayerInfo> playerList = new List<PlayerInfo>();

            error = "";
            try
            {
                sConnection.Open();
                read = sCommand.ExecuteReader();

                while (read.Read())
                {
                    PlayerInfo player = new PlayerInfo();
                    player.FirstName = read["First Name"].ToString();
                    player.Surname = read["Surname"].ToString();
                    player.Position = read["Position"].ToString();
                    player.TeamString = read["Team"].ToString();
                    player.ID = Convert.ToInt32(read["ID"]);
                    playerList.Add(player);
                }
                read.Close();
                
                return playerList;
            }
            catch (Exception e)
            {
                PlayerInfo player = new PlayerInfo();
                player.FirstName = "Error";
                playerList.Add(player);
                error = e.Message;
                return playerList;
            }
            finally
            {
                sConnection.Close();
            }
        }

        // Update player with id
        public int UpdatePlayer(PlayerInfo pi, out string error)
        {
            // SQL Connection created
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True";

            // Query for inserting Player
            String sqlQuery = "UPDATE Tbl_Player SET Pl_First_name = @first, Pl_Surname = @last, Pl_Jersey = @jersey, Pl_Position = @pos, Pl_Birthyear = @birth, Pl_Team = @team WHERE Pl_ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);

            sqlCommand.Parameters.Add("first", SqlDbType.NVarChar, 20).Value = pi.FirstName;
            sqlCommand.Parameters.Add("last", SqlDbType.NVarChar, 20).Value = pi.Surname;
            sqlCommand.Parameters.Add("jersey", SqlDbType.Int).Value = pi.Jersey;
            sqlCommand.Parameters.Add("pos", SqlDbType.NVarChar, 50).Value = pi.Position;
            sqlCommand.Parameters.Add("birth", SqlDbType.Int).Value = pi.BirthYear;
            sqlCommand.Parameters.Add("team", SqlDbType.Int).Value = pi.Team;
            sqlCommand.Parameters.Add("ID", SqlDbType.Int).Value = pi.ID;

            try
            {
                sqlConnection.Open();
                int i = 0;
                i = sqlCommand.ExecuteNonQuery();
                if (i == 1) { error = ""; }
                else { error = "Player not Updated. Rows Affected: " + i; }
                return (i);
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // Get all sponsors
        public List<SponsorInfo> GetSponsors(out string error)
        {
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };

            // Query for getting teams
            String sqlQuery = "SELECT Sp_Name AS Name, Sp_ID AS ID FROM Tbl_Sponsor ";
            SqlCommand sCommand = new SqlCommand(sqlQuery, sConnection);

            SqlDataReader read = null;
            List<SponsorInfo> sponsors= new List<SponsorInfo>();

            error = "";
            try
            {
                sConnection.Open();
                read = sCommand.ExecuteReader();

                while (read.Read())
                {
                    SponsorInfo si = new SponsorInfo();
                    si.Name = read["Name"].ToString();
                    si.ID = Convert.ToInt32(read["ID"]);
                    sponsors.Add(si);
                }
                read.Close();
                return sponsors;
            }
            catch (Exception e)
            {
                SponsorInfo si = new SponsorInfo();
                si.Name = "Error";
                sponsors.Add(si);
                error = e.Message;
                return sponsors;
            }
            finally
            {
                sConnection.Close();
            }
        }

        // Get all players sponsored by sponsor with id
        public List<KeyValuePair<string, int>> GetSponsorsPlayers(int id,out string error)
        {
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };

            // Query for getting teams
            String sqlQuery = "spGetSpPl @ID";
            SqlCommand sCommand = new SqlCommand(sqlQuery, sConnection);

            sCommand.Parameters.Add("ID", SqlDbType.Int).Value = id;
            SqlDataReader read = null;
            List<KeyValuePair<string,int>> players = new List<KeyValuePair<string, int>>();

            error = "";
            try
            {
                sConnection.Open();
                read = sCommand.ExecuteReader();

                while (read.Read())
                {
                    string player;
                    int ID;
                    player = read["Surname"].ToString();
                    ID = Convert.ToInt32(read["ID"]);
                    
                    players.Add(new KeyValuePair<string, int>(player, ID));
                }
                read.Close();
                return players;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
            finally
            {
                sConnection.Close();
            }
        }

        // Get all sponsor - player relations
        public List<KeyValuePair<int,int>> GetRelations(out string error)
        {
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };

            // Query for getting Sponsors
            String sqlQuery = "SELECT Sps_Player AS Player, Sps_Sponsor AS Sponsor FROM Tbl_Sponsors";
            SqlCommand sCommand = new SqlCommand(sqlQuery, sConnection);

            SqlDataReader read = null;
            List<KeyValuePair<int, int>> relations = new List<KeyValuePair<int, int>>();

            error = "";
            try
            {
                sConnection.Open();
                read = sCommand.ExecuteReader();

                while (read.Read())
                {
                    int x,y;
                    x = Convert.ToInt32(read["Sponsor"]);
                    y = Convert.ToInt32(read["Player"]);

                    relations.Add(new KeyValuePair<int,int>(x,y));
                }
                read.Close();
                return relations;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
            finally
            {
                sConnection.Close();
            }
        }

        // Add sponsor - player relation to database
        public int InsertSpRelation(PlayerSponsorBy SpPL, out string error)
        {
            // SQL Connection created
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True";

            // Query for inserting Player
            String sqlQuery = "INSERT INTO Tbl_Sponsors(Sps_Player, Sps_Sponsor) VALUES(@Player, @Sponsor)";
            SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);

            sqlCommand.Parameters.Add("Player", SqlDbType.Int).Value = SpPL.Player;
            sqlCommand.Parameters.Add("Sponsor", SqlDbType.Int).Value = SpPL.Sponsor;

            try
            {
                sqlConnection.Open();
                int i = 0;
                i = sqlCommand.ExecuteNonQuery();
                if (i == 1) { error = ""; }
                else { error = "Could not add relation"; }
                return (i);
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }                                                                                       
}
