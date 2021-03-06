﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Blizzard.Models
{
    public class CharacterModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Faction")]
        public string Faction { get; set; }

        [Required]
        [Display(Name = "Race")]
        public string Race { get; set; }

        [Required]
        [Display(Name = "Class")]
        public string Class { get; set; }

        [Display(Name = "Level")]
        [Range(1, 100, ErrorMessage="The level must be set between 1-100")]
        public short Level { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        private readonly List<string> _factions = new List<string>(Enum.GetNames(typeof(Core.CharacterFaction)));

        public IEnumerable<SelectListItem> FactionItems
        {
            get { return new SelectList(_factions); }
        }

        private readonly List<string> _races = new List<string>(Enum.GetNames(typeof(Core.CharacterRace)));

        public IEnumerable<SelectListItem> RaceItems
        {
            get { return new SelectList(_races); }
        }

        private readonly List<string> _classes = new List<string>(Enum.GetNames(typeof(Core.CharacterClass)));

        public IEnumerable<SelectListItem> ClassItems
        {
            get { return new SelectList(_classes); }
        }
    }

    public class CharacterAddModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Faction")]
        public string Faction { get; set; }

        [Required]
        [Display(Name = "Race")]
        public string Race { get; set; }

        [Required]
        [Display(Name = "Class")]
        public string Class { get; set; }

        private readonly List<string> _factions = new List<string>(Enum.GetNames(typeof(Core.CharacterFaction)));

        public IEnumerable<SelectListItem> FactionItems
        {
            get { return new SelectList(_factions); }
        }

        private readonly List<string> _races = new List<string>(Enum.GetNames(typeof(Core.CharacterRace)));

        public IEnumerable<SelectListItem> RaceItems
        {
            get { return new SelectList(_races); }
        }

        private readonly List<string> _classes = new List<string>(Enum.GetNames(typeof(Core.CharacterClass)));

        public IEnumerable<SelectListItem> ClassItems
        {
            get { return new SelectList(_classes); }
        }
    }
}
