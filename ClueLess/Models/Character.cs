using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Character:Clue
    {
        public  String Color { get; set; }
        public  int StartingLocation { get; set; }
    }
}