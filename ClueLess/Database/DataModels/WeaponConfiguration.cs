using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClueLess.Database.DataModels
{
    public class WeaponConfiguration
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ConfigurationID { get; set; }

        [Required]
        public int WeaponID { get; set; }

      //  [ForeignKey("ConfigurationID")]
        public Configuration Configuration { get; set; }

      //  [ForeignKey("WeaponID")]
        public Weapon Weapon { get; set; }

        public ICollection<PlayerToWeapon> WeaponClues { get; set; }
        public ICollection<Suggestion> Suggestions { get; set; }
        public ICollection<GameSolution> Solutions { get; set; }
    }
}