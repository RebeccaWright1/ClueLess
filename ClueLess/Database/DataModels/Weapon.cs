using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class Weapon
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<WeaponConfiguration> WeaponConfigurations { get; set; }
    }
}