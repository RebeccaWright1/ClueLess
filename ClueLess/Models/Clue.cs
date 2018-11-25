using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Clue
    {
        public Clue() { }

        public  int ID { get; set; }
        public  int ConfigurationID { get; set; }
        public  string Name { get; set; }
        public  String Type { get; set; }
        public  bool IsSolution { get; set; }
    }
}