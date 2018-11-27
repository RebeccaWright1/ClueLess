using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Location:Clue
    {
        public static string LocationType { get; set; }
        public  int SecretDoorRoom { get; set; }
        public  int WeaponID { get; set; }
        public  int ClueID { get; set; } //This is the ID of the clue that is in the room; This is only populated with a 2-player version on the game
        public  int RowPosition { get; set; }
        public  int ColumnPosition { get; set; }
    }
}