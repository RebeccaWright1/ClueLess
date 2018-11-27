using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Player
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int GameID { get; set; }

        [Required]

        public int PositionID { get; set; }

        [Required]
        public int CharacterConfigurationID { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsCurrentPlayer { get; set; }

        [Required]
        public bool IsCurrentRespondingPlayer { get; set; }

       // [ForeignKey("UserID")]
        public virtual User User { get; set; }

      //  [ForeignKey("GameID")]
        public virtual Game Game { get; set; }

       // [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

      //  [ForeignKey("CharacterConfigurationID")]
        public virtual CharacterConfiguration Character{get;set;}

        public virtual ICollection<PlayerToWeapon> WeaponClues { get; set; }
        public virtual ICollection<PlayerToLocation> LocationClues { get; set; }
        public virtual ICollection<PlayerToCharacter> CharacterClues { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }
        public virtual ICollection<ActionTaken> ActionsTaken { get; set; }
        public virtual ICollection<SuggestionResponse> SuggestionResponses { get; set; }



    }
}