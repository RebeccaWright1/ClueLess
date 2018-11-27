using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class ActionTaken
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int PlayerID { get; set; }
        [Required]
        public int ActionID { get; set; }
        
        public int PositionID { get; set; }
        public int SuggestionID { get; set; }
        public int PlayerMovedBySuggestion { get; set; }

       // [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }

      //  [ForeignKey("ActionID")]
        public virtual Actions Action { get; set; }

    //    [ForeignKey("PositionID")]
        public virtual Position Position { get; set; }

    //    [ForeignKey("SuggestionID")]
        public virtual Suggestion Suggestion { get; set; }
        public virtual Player MovedPlayer { get; set; }
    }
}