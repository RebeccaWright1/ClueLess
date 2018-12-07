using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class CharacterConfiguration
    {
        [Key]
        public int ID { get; set; }
        [Required]
        //[ForeignKey("CharacterID")]
        public int CharacterID { get; set; }
        [Required]
        //[ForeignKey("ConfigurationID")]
        public int ConfigurationID { get; set; }

        [Required]
        //[ForeignKey("StartingPositionID")]
        public int StartingPositionID { get; set; }
        [Required]
        public string Color { get; set; }

       // [ForeignKey("CharacterID")]
        public virtual Character Character { get; set; }

     //   [ForeignKey("ConfigurationID")]
        public virtual Configuration Configuration { get; set; }

      //  [ForeignKey("StartingPositionID")]
        public virtual Position StartingPosition { get; set; }
        public virtual ICollection<GameSolution> Solutions{get;set;}
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<PlayerToCharacter> CharacterClues { get; set; }
        public virtual ICollection<Suggestion> Suggestions { get; set; }


    }
}