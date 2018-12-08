using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Position
    {
        [Key]
        public int ID { get; set; }

        [Required]        
        public int ConfigurationID { get; set; }

        [Required]
        public int LocationID { get; set; }

        [Required]
        public int RowPosition { get; set; }

        [Required]
        public int ColumnPosition { get; set; }

       // [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

     //   [ForeignKey("ConfigurationID")]
        public virtual Configuration Configuration{get;set;}
        public virtual ICollection<GameSolution> GameSolutions { get; set; }
        public virtual ICollection<PlayerToLocation> LocationClues { get; set; }
        public virtual ICollection<Suggestion> Suggestion { get; set; }
        public virtual ICollection<ActionTaken> ActionsTaken { get; set; }
        public virtual SecretPassages SecretPassages { get; set; }


    }
}