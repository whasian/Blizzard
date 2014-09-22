using Business;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blizzard.Models
{
    public class PlayerModel
    {
        public Player Player { get; set; }
    }

    public class PlayersModel
    {
        public List<Player> Players { get; set; }
    }
}
