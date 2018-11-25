using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class PlayerToLocation
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PositionID { get; set; }

        [Required]
        public int PlayerID { get; set; }

        [Required]
        public bool WasDealt { get; set; }

       // [ForeignKey("PositionID")]
        public virtual Position LocationClue { get; set; }

      //  [ForeignKey("PlayerID")]
        public virtual Player Player { get; set; }
    }
}