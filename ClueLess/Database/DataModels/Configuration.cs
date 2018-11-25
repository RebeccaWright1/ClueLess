using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Configuration
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool isShared { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<Position> Locations { get; set; }
        public virtual ICollection<CharacterConfiguration> Charaters { get; set; }
        public virtual ICollection<WeaponConfiguration> Weapons { get; set; }

       // [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}