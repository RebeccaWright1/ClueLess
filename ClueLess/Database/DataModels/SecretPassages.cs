using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class SecretPassages
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int PositionID_1 { get; set; }

        [Required]
        public int PositionID_2 { get; set; }

      //  [ForeignKey("PositionID_1")]
        public virtual Position Room1 { get; set; }

     //   [ForeignKey("PositionID_2")]
        public virtual Position Room2 { get; set; }
    }
}