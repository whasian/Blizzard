using System;
using System.IO;
using Core;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class CharacterTest
    {
        #region Create valid Characters
        [TestMethod]
        public void CreateValidCharacterAllianceHumanWarrior()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior);
        }
        
        [TestMethod]
        public void CreateValidCharacterAllianceHumanMage()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Mage);
        }

        [TestMethod]
        public void CreateValidCharacterAllianceHumanDeathKnight()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.DeathKnight);
        }

        [TestMethod]
        public void CreateValidCharacterAllianceGnomeWarrior()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Gnome, CharacterClass.Warrior);
        }

        [TestMethod]
        public void CreateValidCharacterAllianceGnomeMage()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Gnome, CharacterClass.Mage);
        }

        [TestMethod]
        public void CreateValidCharacterAllianceGnomeDeathKnight()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Gnome, CharacterClass.DeathKnight);
        }

        [TestMethod]
        public void CreateValidCharacterAllianceWorgenWarrior()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Worgen, CharacterClass.Warrior);
        }

        [TestMethod]
        public void CreateValidCharacterAllianceWorgenMage()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Worgen, CharacterClass.Mage);
        }

        [TestMethod]
        public void CreateValidCharacterAllianceWorgenDruid()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Worgen, CharacterClass.Druid);
        }

        [TestMethod]
        public void CreateValidCharacterAllianceWorgenDeathKnight()
        {
            new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Worgen, CharacterClass.DeathKnight);
        }

        [TestMethod]
        public void CreateValidCharacterHordeOrcWarrior()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Orc, CharacterClass.Warrior);
        }

        [TestMethod]
        public void CreateValidCharacterHordeOrcMage()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Orc, CharacterClass.Mage);
        }

        [TestMethod]
        public void CreateValidCharacterHordeOrcDeathKnight()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Orc, CharacterClass.DeathKnight);
        }

        [TestMethod]
        public void CreateValidCharacterHordeBloodElfMage()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.BloodElf, CharacterClass.Mage);
        }

        [TestMethod]
        public void CreateValidCharacterHordeBloodElfDeathKnight()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.BloodElf, CharacterClass.DeathKnight);
        }

        [TestMethod]
        public void CreateValidCharacterHordeTaurenWarrior()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Tauren, CharacterClass.Warrior);
        }

        [TestMethod]
        public void CreateValidCharacterHordeTaurenMage()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Tauren, CharacterClass.Mage);
        }

        [TestMethod]
        public void CreateValidCharacterHordeTaurenDruid()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Tauren, CharacterClass.Druid);
        }

        [TestMethod]
        public void CreateValidCharacterHordeTaurenDeathKnight()
        {
            new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Tauren, CharacterClass.DeathKnight);
        }
        #endregion

        [TestMethod]
        public void CreateInvalidCharacterNoName()
        {
            // no name
            try
            {
                new Character(null, CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior);
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, "Character must have a name");
            }

            try
            {
                new Character("", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Character must have a name");
            }
        }

        #region Invalid Faction Race Combo
        [TestMethod]
        public void CreateInvalidCharacterAllianceOrc()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Orc, CharacterClass.Warrior);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Only Humans, Gnomes, and Worgen can fight for the Alliance");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterAllianceTauren()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Tauren, CharacterClass.Warrior);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Only Humans, Gnomes, and Worgen can fight for the Alliance");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterAllianceBloodElf()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.BloodElf, CharacterClass.Mage);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Only Humans, Gnomes, and Worgen can fight for the Alliance");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterHordeHuman()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Human, CharacterClass.Warrior);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Only Orcs, Taurens, and Blood Elfs can fight for the Horde");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterHordeGnome()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Gnome, CharacterClass.Warrior);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Only Orcs, Taurens, and Blood Elfs can fight for the Horde");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterHordeWorgen()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Worgen, CharacterClass.Warrior);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Only Orcs, Taurens, and Blood Elfs can fight for the Horde");
            }
        }
        #endregion

        #region Invalid Race Class Combo
        [TestMethod]
        public void CreateInvalidCharacterAllianceHumanDruid()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Druid);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "This race cannot be a Druid");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterAllianceGnomeDruid()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Gnome, CharacterClass.Druid);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "This race cannot be a Druid");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterHordeOrcDruid()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Orc, CharacterClass.Druid);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "This race cannot be a Druid");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterAllianceBloodElfDruid()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.BloodElf, CharacterClass.Druid);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "This race cannot be a Druid");
            }
        }

        [TestMethod]
        public void CreateInvalidCharacterAllianceBloodElfWarrior()
        {
            try
            {
                new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.BloodElf, CharacterClass.Warrior);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Blood Elfs cannot be Warriors");
            }
        }
        #endregion

        [TestMethod]
        public void CreateValidCharacterDeleteUndelete()
        {
            Character c = new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior);

            Assert.IsTrue(c.Active);

            c.Delete();

            Assert.IsFalse(c.Active);

            c.Undelete();

            Assert.IsTrue(c.Active);
        }
    }
}
