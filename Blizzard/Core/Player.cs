using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Core
{
    public class Player
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Character> Characters { get; set; }

        public Player()
        {
        }

        public Player(string userName, string password)
        {
            if(String.IsNullOrEmpty(userName))
            {
                throw new Exception("Player must have a username");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new Exception("Player must have a password");
            }

            this.UserName = userName;
            this.Password = password;
            this.Characters = new List<Character>();
        }

        public void AddCharacter(Character character)
        {
            if(character.Class == CharacterClass.DeathKnight)
            {
                if(!this.Characters.Exists(x => x.Level >= 55 && x.Active))
                {
                    throw new Exception("Cannot create a Death Knight untill one of your Characters is at least level 55");
                }
            }

            if(this.Characters.Count > 0 && this.Characters.Exists(x => x.Faction != character.Faction && x.Active))
            {
                throw new Exception("Stay loyal to your faction and only create characters within the same faction");
            }

            this.Characters.Add(character);
        }

        public void EditCharacter(Character character)
        {
            Character c = this.Characters.FirstOrDefault(x => x.Id == character.Id);

            if (c == null)
            {
                throw new Exception("This Character does not exist");
            }

            if (this.Characters.Count > 0 && this.Characters.Exists(x => character.Active && x.Faction != character.Faction && x.Active))
            {
                throw new Exception("Stay loyal to your faction and only create characters within the same faction");
            }

            c.Name = character.Name;
            c.Faction = character.Faction;
            c.Race = character.Race;
            c.Class = character.Class;
            c.Level = character.Level;
            c.Active = character.Active;
        }

        public void Delete(Guid characterId)
        {
            Character c = this.Characters.FirstOrDefault(x => x.Id == characterId);

            if(c == null)
            {
                throw new Exception("This Character does not exist");
            }
            if(c.Active == false)
            {
                throw new Exception("This Character has already been deleted");
            }

            c.Delete();
        }

        public void Undelete(Guid characterId)
        {
            Character c = this.Characters.FirstOrDefault(x => x.Id == characterId);

            if (c == null)
            {
                throw new Exception("This Character does not exist");
            }
            if (c.Active == true)
            {
                throw new Exception("This Character is already undeleted");
            }

            c.Undelete();
        }
    }
}
