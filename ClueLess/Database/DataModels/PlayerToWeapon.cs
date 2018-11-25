using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class PlayerToWeapon
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PlayerID { get; set; }

        [Required]
        public int WeaponConfigurationID { get; set; }

        [Required]
        public bool WasDealt { get; set; }

       // [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

       // [ForeignKey("WeaponConfigurationID")]
        public virtual WeaponConfiguration WeaponClue { get; set; }

    }
}