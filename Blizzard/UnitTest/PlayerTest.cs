using System;
using Core;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void CreateValidPlayer()
        {
            new Player();

            new Player("testName", "testPassword");
        }

        [TestMethod]
        public void CreateInvalidPlayer()
        {
            try
            {
                new Player("testName", null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Player must have a password");
            }

            try
            {
                new Player("testName", "");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Player must have a password");
            }

            try
            {
                new Player(null, "testPassword");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Player must have a username");
            }

            try
            {
                new Player("", "testPassword");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Player must have a username");
            }            
        }

        #region Character Add Tests
        [TestMethod]
        public void AddCharater()
        {
            Player p = new Player("testName", "testPassword");

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior));
        }

        [TestMethod]
        public void AddValidDeathKnight()
        {
            Player p = new Player("testName", "testPassword");

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior));

            p.Characters.First().Level = 55;

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.DeathKnight));

        }

        [TestMethod]
        public void AddInvalidDeathKnight()
        {
            Player p = new Player("testName", "testPassword");

            try
            {
                p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.DeathKnight));
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, "Cannot create a Death Knight untill one of your Characters is at least level 55");
            }
            
        }

        [TestMethod]
        public void AddValidFactionCharacter()
        {
            Player p = new Player("testName", "testPassword");

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Orc, CharacterClass.Warrior));

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Tauren, CharacterClass.Druid));
        }

        [TestMethod]
        public void AddInvalidFactionCharacter()
        {
            Player p = new Player("testName", "testPassword");

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Horde, CharacterRace.Orc, CharacterClass.Warrior));

            try
            {
                p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Stay loyal to your faction and only create characters within the same faction");
            }
        }
        #endregion

        [TestMethod]
        public void ValidDelete()
        {
            Player p = new Player("testName", "testPassword");

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior));

            p.Delete(p.Characters.First().Id);
        }

        [TestMethod]
        public void InvalidDeleteNonexistant()
        {
            Player p = new Player("testName", "testPassword");

            try
            {
                p.Delete(Guid.NewGuid());
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.AreEqual(ex.Message, "This Character does not exist");
            }
        }

        [TestMethod]
        public void InvalidDeleteAlreadyDeleted()
        {
            Player p = new Player("testName", "testPassword");

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior));

            try
            {
                p.Delete(p.Characters.First().Id);
                p.Delete(p.Characters.First().Id);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "This Character has already been deleted");
            }
        }

        [TestMethod]
        public void ValidUndelete()
        {
            Player p = new Player("testName", "testPassword");

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior));

            p.Characters.First().Active = false;

            p.Undelete(p.Characters.First().Id);
        }

        [TestMethod]
        public void InvalidUndeleteNonexistant()
        {
            Player p = new Player("testName", "testPassword");

            try
            {
                p.Undelete(Guid.NewGuid());
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "This Character does not exist");
            }
        }

        [TestMethod]
        public void InvalidUndeleteAlreadyUndeleted()
        {
            Player p = new Player("testName", "testPassword");

            p.AddCharacter(new Character("TestCharacter", CharacterFaction.Alliance, CharacterRace.Human, CharacterClass.Warrior));

            try
            {
                p.Undelete(p.Characters.First().Id);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "This Character is already undeleted");
            }
        }
    }
}
