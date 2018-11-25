using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class PlayerToCharacter
    {
        [Key]
        public int ID { get; set; }

        [Required]
       // [ForeignKey("PlayerID")]
        public int PlayerID { get; set; }

        [Required]
        public int CharacterConfigurationID { get; set; }

        [Required]
        public bool WasDealt { get; set; }

       // [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

     //   [ForeignKey("CharacterConfigurationID")]
        public virtual CharacterConfiguration CharacterClue { get; set; }
    }
}