using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Game
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ConfigurationID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Status Status{get;set;}
        [Required]
        public int UserID { get; set; }

       // [ForeignKey("UserID")]
        public virtual User User { get; set; }

      //  [ForeignKey("ConfigurationID")]
        public virtual Configuration Configuration { get; set; }
        public virtual ICollection<WeaponPosition> Weapons { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual GameSolution Solution { get; set; }
    }

    public enum Status
    {
        New=1,
        InProgress=2,
        Completed_Solved=3,
        Completed_Unsolved=4

    }
}