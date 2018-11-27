using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Player
    {
        public Player() { }
        public Player(int id, int userID, int characterID)
        {
            //Step 1. Connect to the database
            //Step 2. Create the (db) player in the database and save changes
            //Step 3. Update the player's information
        }

        public static void ProposeScenario(Suggestion suspectScenario) {
            throw new NotImplementedException();
        }

        public static void MoveToLocation(int locationID, int characterid)
        {
            throw new NotImplementedException();
        }

        public static void SignalEndofTurn()
        {
            throw new NotImplementedException();
        }

        public static String RespondToSuggestion()
        {
            return "Not Implemented";
        }



        public  int CharacterID { get; set; }
        public  bool IsActive { get; set; }
        public  bool IsCurrentPlayer { get; set; }
        public  int ID { get; set; }
        public  int UserID { get; set; }
        public  int LocationID { get; set; }
        public  List<Clue> TrackedClues { get; set; }
        public  bool IsCurrentResponder { get; set; }
    }
}