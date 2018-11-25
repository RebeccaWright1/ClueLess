using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Character
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int Name { get; set; }

        public virtual ICollection<CharacterConfiguration> CharacterConfiguration { get; set; }
    }
}