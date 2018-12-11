using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class SuggestionResponse
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int SuggestionID { get; set; }

        [Required]
        public int PlayerID { get; set; }

        [Required]
        public string Response { get; set; }
        public int RevealedClueID { get; set; }
        public string RevealedClueTable { get; set; }

        //[ForeignKey("SuggestionID")]
        public virtual Suggestion Suggestion { get; set; }

      //  [ForeignKey("PlayerID")]
        public virtual Player RespondingPlayer { get; set; }
    }
}