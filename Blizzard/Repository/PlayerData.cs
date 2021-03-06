﻿using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PlayerData
    {
        private string FILENAME { get; set; }
        private const string SALT = "thisIsASaltHeHe";
        private object fileLock = new object();

        public PlayerData(string loginFile)
        {
            FILENAME = loginFile;
        }

        public void AddPlayer(string userName, string password)
        {
            lock (fileLock)
            {
                List<Player> players = GetAllPlayers();

                if (players.Exists(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("This User already exists");
                }

                players.Add(new Player(userName, hashPassword(password)));

                Save(players);
            }
        }

        public void EditPlayer(string userName, bool isAdmin)
        {
            lock (fileLock)
            {
                List<Player> players = GetAllPlayers();

                Player p = players.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (p == null)
                {
                    throw new Exception("This User doesn't exists");
                }

                p.IsAdmin = isAdmin;

                Save(players);
            }
        }

        public Player GetPlayer(string userName, string password)
        {
            lock (fileLock)
            {
                Player p = null;

                List<Player> players = GetAllPlayers();

                p = players.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (p == null)
                {
                    throw new Exception("This User does not exist");
                }

                if (p.Password != hashPassword(password))
                {
                    throw new Exception("The password is incorrect");
                }

                return p;
            }
        }

        public Player GetPlayer(string userName)
        {
            lock (fileLock)
            {
                Player p = null;
                List<Player> players = GetAllPlayers();

                p = players.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (p == null)
                {
                    throw new Exception("This User does not exist");
                }
                return p;
            }
        }

        public List<Player> GetPlayer()
        {
            lock (fileLock)
            {
                return GetAllPlayers();
            }
        }

        public void AddCharacter(string userName, Character character)
        {
            lock (fileLock)
            {
                List<Player> players = GetAllPlayers();

                if (!players.Exists(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("This User doesn't exists");
                }

                players.First(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)).AddCharacter(character);

                Save(players);
            }
        }

        public void EditCharacter(string userName, Character character)
        {
            lock (fileLock)
            {
                List<Player> players = GetAllPlayers();

                Player p = players.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (p == null)
                {
                    throw new Exception("This User doesn't exists");
                }

                p.EditCharacter(character);

                Save(players);
            }
        }

        public Character GetCharacter(string userName, Guid Id)
        {
            lock (fileLock)
            {
                List<Player> players = GetAllPlayers();

                Player p = players.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (p == null)
                {
                    throw new Exception("This User does not exist");
                }

                Character c = p.Characters.FirstOrDefault(x => x.Id == Id);

                if(c == null)
                {
                    throw new Exception("This Character does not exist");
                }

                return c;
            }
        }

        public List<Character> GetCharacter(string userName)
        {
            lock (fileLock)
            {
                List<Player> players = GetAllPlayers();

                Player p = players.FirstOrDefault(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

                if (p == null)
                {
                    throw new Exception("This User does not exist");
                }

                return p.Characters;
            }
        }

        private List<Player> GetAllPlayers()
        {
            List<Player> players = JsonConvert.DeserializeObject<List<Player>>(System.IO.File.ReadAllText(FILENAME));

            if (players == null)
            {
                players = new List<Player>();
            }

            return players;
        }

        private void Save(List<Player> players)
        {
            string json = JsonConvert.SerializeObject(players, Formatting.Indented);

            //write string to file
            System.IO.File.WriteAllText(FILENAME, json);
        }

        private string hashPassword(string password)
        {
            string sHashWithSalt = password + SALT;
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            System.Security.Cryptography.HashAlgorithm algorithm = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);

            return System.Text.Encoding.Default.GetString(hash);
        }
    }
}
