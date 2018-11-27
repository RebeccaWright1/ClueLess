using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class GameSolution
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int GameID { get; set; }

        [Required]
        public int ConfiguredWeaponID { get; set; }

        [Required]
        public int ConfiguredCharacterID { get; set; }

        [Required]
        public int PositionID { get; set; }

        public int SolvedByPlayerID { get; set; }

        [Required]
        //[ForeignKey("GameID")]
        public virtual Game Game { get; set; }

       // [ForeignKey("ConfiguredWeaponID")]
        public virtual WeaponConfiguration Weapon { get; set; }

      //  [ForeignKey("ConfiguredCharacterID")]
        public virtual CharacterConfiguration Character { get; set; }

      //  [ForeignKey("PositionID")]
        public virtual Position Location { get; set; }
        public virtual Player Winner { get; set; }
    }
}