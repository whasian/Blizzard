using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public enum CharacterRace { Orc, Tauren, BloodElf, Human, Gnome, Worgen }
    public enum CharacterClass { Warrior, Druid, DeathKnight, Mage }
    public enum CharacterFaction { Horde, Alliance }

    public class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        private short _level;
        public short Level
        {
            get
            {
                return _level;
            }
            set
            {
                if(value < 1 || value > 100)
                {
                    throw new Exception("Invalid level");
                }
                _level = value;
            }
        }
        public CharacterFaction Faction { get; set; }
        public CharacterRace Race { get; set; }
        public CharacterClass Class { get; set; }
        public bool Active { get; set; }

        public Character(string name, CharacterFaction characterFaction, CharacterRace characterRace, CharacterClass characterClass)
        {
            ValidateCharacter(characterFaction, characterRace, characterClass);

            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Faction = characterFaction;
            this.Race = characterRace;
            this.Class = characterClass;
            this.Active = true;
            
            if(characterClass == CharacterClass.DeathKnight)
            {
                this.Level = 55;
            }
            else
            {
                this.Level = 1;
            }
        }

        public void Delete()
        {
            this.Active = false;
        }

        public void Undelete()
        {
            this.Active = true;
        }

        private void ValidateCharacter(CharacterFaction characterFaction, CharacterRace characterRace, CharacterClass characterClass)
        {
            #region Faction Validation
            
            if(characterFaction == CharacterFaction.Horde)
            {
                if(characterRace != CharacterRace.Orc &&
                   characterRace != CharacterRace.Tauren &&
                   characterRace != CharacterRace.BloodElf)
                {
                    throw new Exception("Only Orcs, Taurens, and Blood Elfs can fight for the Horde");
                }
            }
            else
            {
                if (characterRace != CharacterRace.Human &&
                   characterRace != CharacterRace.Gnome &&
                   characterRace != CharacterRace.Worgen)
                {
                    throw new Exception("Only Humans, Gnomes, and Worgen can fight for the Alliance");
                }
            }

            #endregion

            #region Race Validation
            
            if(characterClass == CharacterClass.Druid)
            {
                if(characterRace != CharacterRace.Tauren &&
                   characterRace != CharacterRace.Worgen)
                {
                    throw new Exception("This race cannot be a Druid");
                }
            }

            if(characterRace == CharacterRace.BloodElf && characterClass == CharacterClass.Warrior)
            {
                throw new Exception("Blood Elfs cannot be Warriors");
            }

            #endregion
        }
    }
}
