using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostawNaMilion.Models
{
    public interface IPlayerData
    {
        public Player Player { get; set; }
        Player CreateNewPlayer(Player player);
        IEnumerable<Player> GetTopPlayersScore();
        void AddToDatabase();
        void ResetData();
    }
}
