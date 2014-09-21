using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Business;

namespace BusinessRules
{
    public class CharacterRules
    {
        private string FileName { get; set; }
        private string UserName { get; set; }

        public CharacterRules(string loginFile, string userName)
        {
            this.FileName = loginFile;
            this.UserName = userName;
        }

        public void AddCharacter(string name, string faction, string race, string characterclass)
        {
            CharacterFaction cf;
            CharacterRace cr;
            CharacterClass cc;
            
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

            if(cc == CharacterClass.DeathKnight && !GetCharacter().Exists(x => x.Active && x.Level >= 55))
            {
                throw new Exception("Must have another Character level 55 or higher to be a Death Knight");
            }

            Character newCharacter = new Character(name, cf, cr, cc);
            
            PlayerData p = new PlayerData(FileName);
            p.AddCharacter(UserName, newCharacter);
        }

        public void EditCharacter(Guid id, string name, string faction, string race, string characterclass)
        {
            CharacterFaction cf;
            CharacterRace cr;
            CharacterClass cc;

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

            if(cc == CharacterClass.DeathKnight && !GetCharacter().Exists(x => x.Active && x.Level >= 55))
            {
                throw new Exception("Must have another Character level 55 or higher to be a Death Knight");
            }

            Character newCharacter = new Character(name, cf, cr, cc);

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
