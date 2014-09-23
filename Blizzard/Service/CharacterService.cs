using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Core;

namespace Services
{
    public class CharacterService
    {
        private string FileName { get; set; }
        private string UserName { get; set; }

        public CharacterService(string loginFile, string userName)
        {
            this.FileName = loginFile;
            this.UserName = userName;
        }

        public void AddCharacter(string name, string faction, string race, string characterclass)
        {
            CharacterFaction cf;
            CharacterRace cr;
            CharacterClass cc;

            List<Character> characters = GetCharacter();
            
            if(!Enum.TryParse<CharacterFaction>(faction, out cf))
            {
                throw new Exception("Faction invalid");
            }

            if(!Enum.TryParse<CharacterRace>(race, out cr))
            {
                throw new Exception("Race invalid");
            }

            if(!Enum.TryParse<CharacterClass>(characterclass, out cc))
            {
                throw new Exception("Class invalid");
            }

            if(cc == CharacterClass.DeathKnight && !characters.Exists(x => x.Active && x.Level >= 55))
            {
                throw new Exception("Must have another Character level 55 or higher to be a Death Knight");
            }

            if(characters.Count(x => x.Faction != cf && x.Active == true) > 0)
            {
                throw new Exception("All Characters must be part of the " + characters.First().Faction.ToString());
            }

            Character newCharacter = new Character(name, cf, cr, cc);
            
            PlayerData p = new PlayerData(FileName);
            p.AddCharacter(UserName, newCharacter);
        }

        public void EditCharacter(Guid id, string name, string faction, string race, string characterclass, short level, bool Active)
        {
            CharacterFaction cf;
            CharacterRace cr;
            CharacterClass cc;

            List<Character> characters = GetCharacter();

            if (!Enum.TryParse<CharacterFaction>(faction, out cf))
            {
                throw new Exception("Faction invalid");
            }

            if (!Enum.TryParse<CharacterRace>(race, out cr))
            {
                throw new Exception("Race invalid");
            }

            if (!Enum.TryParse<CharacterClass>(characterclass, out cc))
            {
                throw new Exception("Class invalid");
            }

            //if(cc == CharacterClass.DeathKnight && !characters.Exists(x => x.Active && x.Level >= 55))
            //{
            //    throw new Exception("Must have another Character level 55 or higher to be a Death Knight");
            //}

            if (characters.Count(x => Active == true && x.Faction != cf && x.Id != id && x.Active == true) > 0)
            {
                throw new Exception("All Characters must be part of the " + characters.First().Faction.ToString());
            }

            Character newCharacter = new Character(name, cf, cr, cc);
            newCharacter.Level = level;
            newCharacter.Active = Active;

            PlayerData p = new PlayerData(FileName);
            p.EditCharacter(UserName, id, newCharacter);
        }

        public Character GetCharacter(Guid id)
        {
            PlayerData p = new PlayerData(FileName);
            return p.GetCharacter(UserName, id);
        }

        public List<Character> GetCharacter()
        {
            PlayerData p = new PlayerData(FileName);
            return p.GetCharacter(UserName);
        }
    }
}
