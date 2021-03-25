using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
    public class PlayerData : IPlayerData
    {
        public Player Player { get; set; }

        public IConfiguration Configuration { get; }
        public readonly string ConnString;
        public PlayerData(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnString = Configuration.GetConnectionString("Database");
        }

        public Player CreateNewPlayer(Player player)
        {
            Player = new Player
            {
                Name = player.Name,
                Surname = player.Surname,
                IsUsedMoreTime = false
            };
            return Player;
        }

        public IEnumerable<Player> GetTopPlayersScore()
        {
            List<Player> players = new List<Player>();
            //change to your connString
    
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                con.Open();
                var cmd = new SqlCommand("SELECT * FROM dbo.Player ORDER BY Points;", con);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            Player player = new Player();
                            player.Name = reader["Name"].ToString();
                            player.Surname = reader["Surname"].ToString();
                            player.Points = Convert.ToInt32(reader["Points"]);
                            players.Add(player);
                        }
                    }
                }
            }
            return players;
        }

        public void AddToDatabase()
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {

                con.Open();
                SqlCommand playersCount = new SqlCommand("SELECT COUNT(*) FROM dbo.Player;", con);
                int count = Convert.ToInt32(playersCount.ExecuteScalar());
                count++;
                string InsertValues = $"INSERT INTO dbo.Player VALUES ({count}, '{QuestionDatas.Player.Name}', '{QuestionDatas.Player.Surname}', {Player.Amount});";
                var addData = new SqlCommand(InsertValues, con);
                addData.ExecuteNonQuery();
                con.Close();
            }
        }

        public void ResetData()
        {
            QuestionDatas.Player = null;
            Player.Amount = 1000000;
        }

    }
}
