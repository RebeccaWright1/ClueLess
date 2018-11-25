using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Suggestion
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PlayerID { get; set; }

        [Required]
        public int LocationID { get; set; }

        [Required]
        public int ConfiguredWeaponID { get; set; }

        [Required]
        public int ConfiguredCharacterID { get; set; }

        [Required]
        public bool IsAccusation { get; set; }

      //  [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

     //   [ForeignKey("LocationID")]
        public virtual Position Location { get; set; }

    //    [ForeignKey("ConfiguredWeaponID")]
        public virtual WeaponConfiguration Weapon { get; set; }

     //   [ForeignKey("ConfiguredCharacterID")]
        public virtual CharacterConfiguration Character { get; set; }
        public virtual ICollection<SuggestionResponse>Reponses{get;set;}
    }
}