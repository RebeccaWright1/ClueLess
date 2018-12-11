using ClueLess.Database;
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

        public static void SignalEndofTurn(int playerID)
        {
           
            using(ClueLessContext db= new ClueLessContext())
            {
                Database.DataModels.Player currentPlayer = db.Players.Where(x => x.ID == playerID).FirstOrDefault();
                int NextPlayerID = FindNextPlayer(currentPlayer.GameID);
                Database.DataModels.Player nextPlayer = db.Players.Where(x => x.ID == NextPlayerID).FirstOrDefault();

                currentPlayer.IsCurrentPlayer = false;
                nextPlayer.IsCurrentPlayer = true;

                db.SaveChanges();
            }
        }

        private static int FindNextPlayer(int gameID)
        {
            ClueLessContext db = new ClueLessContext();
            int nextPlayerID;
            //Pull the list of all the players
            List<Database.DataModels.Player> players = db.Players.Where(x => x.GameID == gameID && x.IsActive==true).OrderBy(x=>x.ID).ToList();

            //Get the index of the current player
            int currentIndex = players.IndexOf(players.Where(x => x.IsCurrentPlayer).FirstOrDefault());

            if (currentIndex == players.Count-1)
            {
                 nextPlayerID = players.ElementAt(0).ID;
            }
            else
            {
                 nextPlayerID = players.ElementAt(currentIndex + 1).ID;
            }

            return nextPlayerID;
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