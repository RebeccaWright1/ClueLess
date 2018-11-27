using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class WeaponPosition
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int ConfiguredWeaponID { get; set; }

        [Required]
        public int GameID { get; set; }

        [Required]
        public int PositionID { get; set; }

      //  [ForeignKey("GameID")]
        public virtual Game Game { get; set; }

     //   [ForeignKey("ConfiguredWeaponID")]
        public virtual WeaponConfiguration ConfiguredWeapon { get; set; }

    //    [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }
    }
}