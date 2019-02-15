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
        public PlayerMethods() { }
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
        
        public List<PlayerInfo> GetPlayerInfo(out string error)
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

        public PlayerInfo GetPlayer(int id, out string error)
        {
            int newID = id;
            // SQL Connection created
            SqlConnection sConnection = new SqlConnection
            {
                ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Fotboll;Integrated Security=True"
            };

            // Query for getting teams
            String sqlQuery = "SELECT PL_ID AS ID,Pl_First_name AS [First Name], Pl_Surname AS [Surname], Pl_Position AS Position, Pl_Jersey AS Jersey, Pl_Birthyear AS Birthyear,Te_Name  AS Team From Tbl_Player INNER JOIN Tbl_Team ON Te_ID = Pl_Team"
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
    }                                                                                       
}
