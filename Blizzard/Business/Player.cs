using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace Business
{
    public class Player
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Character> Characters { get; set; }
        
        public Player(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
            this.Characters = new List<Character>();
        }

        public Character AddCharacter(string name, CharacterFaction characterFaction, CharacterRace characterRace, CharacterClass characterClass)
        {
            if(characterClass == CharacterClass.DeathKnight)
            {
                if(!this.Characters.Exists(x => x.Level >= 55))
                {
                    throw new Exception("Cannot create a Death Knight untill one of your Characters is at least level 55");
                }
            }

            if(this.Characters.Count > 0 && this.Characters.Exists(x => x.Faction == characterFaction))
            {
                throw new Exception("Stay loyal to your faction and only create characters within the same faction");
            }

            return new Character(name, characterFaction, characterRace, characterClass);
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
