using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Location
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public LocationType LocationType { get; set; }
        [Required]
        public string LocationName { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
    }

    public enum LocationType
    {
        Room=0,
        Hallway=1
    }
}