using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClueLess.Database.DataModels
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        
        [Required]
        public string Username { get; set; }
        public string Avatar { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsAdminsitrator { get; set; }

        public virtual ICollection<Configuration> Configuration { get; set; }
        public virtual ICollection<Game> Game { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}