using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class PositionToClue
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PositionID { get; set; }

        [Required]
        public int ClueID { get; set; }

        [Required]
        public string ClueType { get; set; }

        [Required]
        public int GameID { get; set; }
    }
}