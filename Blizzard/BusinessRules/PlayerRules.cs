using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Business;

namespace BusinessRules
{
    public class PlayerRules
    {
        private string FileName { get; set; }

        public PlayerRules(string loginFile)
        {
            this.FileName = loginFile;
        }

        public void AddPlayer(string userName, string password)
        {
            PlayerData p = new PlayerData(FileName);
            p.AddPlayer(userName, password);
        }

        public void EditPlayer(string userName, bool isAdmin)
        {
            PlayerData p = new PlayerData(FileName);
            p.EditPlayer(userName, isAdmin);
        }

        public Player GetPlayer(string userName, string password)
        {
            PlayerData p = new PlayerData(FileName);
            return p.GetPlayer(userName, password);
        }

        public Player GetPlayer(string userName)
        {
            PlayerData p = new PlayerData(FileName);
            return p.GetPlayer(userName);
        }

        public List<Player> GetPlayer()
        {
            PlayerData p = new PlayerData(FileName);
            return p.GetPlayer();
        }
    }
}
