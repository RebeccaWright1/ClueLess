using System.Collections.Generic;

namespace ClueLess.Models
{
    public class BoardLocation:Location
    {

        public static Clue RevealClue()
        {
            return new Clue();
        }

        public static bool DetermineAvailability()
        {
            if( LocationType=="Hallway" && Occupants.Count > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public static List<Player> Occupants { get; set; }
    }
}