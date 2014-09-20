using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Character;

namespace Business.Player
{
    public class Player
    {
        public List<Character.Character> Characters { get; set; }
        
        public Player()
        {

        }

        public Character.Character AddCharacter(string name, CharacterFaction characterFaction, CharacterRace characterRace, CharacterClass characterClass)
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

            return new Character.Character(name, characterFaction, characterRace, characterClass);
        }

        public void Delete(Guid characterId)
        {
            Character.Character c = this.Characters.FirstOrDefault(x => x.Id == characterId);

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
            Character.Character c = this.Characters.FirstOrDefault(x => x.Id == characterId);

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
