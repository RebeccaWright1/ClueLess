using ClueLess.Database;
using ClueLess.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClueLess.Models
{
    public class Suggestion
    {
        public  bool ValidateAccusation()
        {
            bool isValid = false;
            try
            {
                using(ClueLessContext db= new ClueLessContext())
                {
                    //Get the datbase suggestion object
                    Database.DataModels.Suggestion suggestion = db.Suggestions.Where(x => x.ID == ID).FirstOrDefault();

                    //Get the gameID
                    int gameID = db.Players.Where(x => x.ID == suggestion.PlayerID).Select(x => x.GameID).FirstOrDefault();

                    //Pull the game's solution
                    Database.DataModels.GameSolution solution = db.GameSolutions.Where(x => x.GameID == gameID).FirstOrDefault();

                    if (solution.ConfiguredCharacterID == suggestion.ConfiguredCharacterID && 
                        solution.ConfiguredWeaponID == suggestion.ConfiguredWeaponID && 
                        solution.PositionID == suggestion.LocationID)
                    {
                        isValid = true;
                        //End the Game
                        solution.SolvedByPlayerID = suggestion.PlayerID;
                        db.SaveChanges();

                        Game.ChangeGameStatus(gameID, Database.DataModels.Status.Completed_Solved);
                    }                       
                    else
                    {
                        //determine the number of active players
                        int activePlayers = db.Players.Where(x => x.GameID == gameID && x.IsActive).Count();
                        if (activePlayers > 2)
                        {
                            //deactivate the player
                            Database.DataModels.Player player = db.Players.Where(x => x.ID == playerID).FirstOrDefault();
                            player.IsActive = false;
                            db.SaveChanges();
                        }
                        else
                        {
                            //end the game
                            Game.ChangeGameStatus(gameID, Database.DataModels.Status.Completed_Unsolved);
                        }

                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
            }
            return isValid;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerID">the ID of the player revealing the clue</param>
        /// <param name="clueID">The ID of the clue being revealed</param>
        /// <param name="ClueType">The Type of clue being revealed: Character, Weapon, Room</param>
        public static  Clue RevealClue(int playerID, int clueID, string ClueType, int suggestionID)
        {
            Clue revealedClue = new Clue();
            try
            {
               
                using(ClueLessContext db= new ClueLessContext())
                {
                    //Get the database suggestion object
                    Database.DataModels.Suggestion suggestion = db.Suggestions.Where(x => x.ID == suggestionID).FirstOrDefault();

                    //Update the suggestion's author's clue tracker based on the clue type
                    switch (ClueType)
                    {
                        case "Character":
                            db.PlayersToCharcters.Add(new Database.DataModels.PlayerToCharacter
                            {
                                PlayerID = suggestion.PlayerID,
                                CharacterConfigurationID=clueID,
                            });
                            db.SaveChanges();

                            //Pull the information for the clue to be returned
                            CharacterConfiguration characterConfiguration = db.CharacterConfigurations.Where(x => x.ID == clueID).FirstOrDefault();
                            revealedClue = new Clue
                            {
                                ID = characterConfiguration.ID,
                                Name = characterConfiguration.Character.Name,
                            };

                            break;
                        case "Weapon":
                            db.PlayersToWeapons.Add(new Database.DataModels.PlayerToWeapon
                            {
                                PlayerID = suggestion.PlayerID,
                                WeaponConfigurationID=clueID
                            });
                            db.SaveChanges();

                            WeaponConfiguration weaponConfiguration = db.WeaponConfigurations.Where(x => x.ID == clueID).FirstOrDefault();
                            revealedClue = new Clue { ID = weaponConfiguration.ID, Name = weaponConfiguration.Weapon.Name };

                            break;
                        case "Room":
                            db.PlayersToLocations.Add(new Database.DataModels.PlayerToLocation
                            {
                                PlayerID = suggestion.PlayerID,
                                PositionID = clueID
                            });
                            db.SaveChanges();

                            //Pull the information for the clue to be returned
                            Position position = db.Positions.Where(x => x.ID == clueID).FirstOrDefault();
                            revealedClue = new Clue { ID = position.ID, Name = position.Location.LocationName };

                            break;
                        default:
                            throw new ArgumentException();

                    }

                    
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
            }

            return revealedClue;
        }

        public void Save()
        {
            using (ClueLessContext db =new ClueLessContext())
            {
                Database.DataModels.Suggestion suggestion= new Database.DataModels.Suggestion
                {
                    LocationID = LocationID,
                    PlayerID = playerID,
                    ConfiguredCharacterID = characterID,
                    ConfiguredWeaponID = weaponID,
                    IsAccusation = IsAccusastion
                };
                db.Suggestions.Add(suggestion);

                db.SaveChanges();

                //set the player to be the current responding player
                Database.DataModels.Player author = db.Players.Where(x => x.ID == suggestion.PlayerID).FirstOrDefault();
                author.IsCurrentRespondingPlayer = true;
                db.SaveChanges();

                int firstResponderID = FindNextResponder(suggestion.ID);
                Database.DataModels.Player firstResponder = db.Players.Where(x => x.ID == firstResponderID).FirstOrDefault();
                firstResponder.IsCurrentRespondingPlayer = true;
                author.IsCurrentRespondingPlayer = false;
                db.SaveChanges();
            }
        }

        public static Clue RespondToSuggestion(int playerId, int suggestionID, int clueID=-1, string ClueType="")
        {
            Clue responseClue = new Clue();
            SuggestionResponse response;
            ClueLessContext db = new ClueLessContext();
            if (clueID >0)
            {
                responseClue = RevealClue(playerId, clueID, ClueType, suggestionID);
                response = new SuggestionResponse
                {
                    SuggestionID = suggestionID,
                    Response = "Revealed Clue",
                    RevealedClueID=clueID,
                    RevealedClueTable=ClueType
                };
                db.SaveChanges();
            }
            else
            {
                response = new SuggestionResponse
                {
                    SuggestionID = suggestionID,
                    Response = "Pass"
                };
                db.SaveChanges();

                Database.DataModels.Suggestion suggestion= db.Suggestions.Where(x => x.ID == suggestionID).FirstOrDefault();
                int nextPlayerID = FindNextResponder(suggestionID);
                Database.DataModels.Player currentResponder = db.Players.Where(x => x.ID == suggestion.PlayerID).FirstOrDefault();
                currentResponder.IsCurrentRespondingPlayer = false;
                db.SaveChanges();
                if (nextPlayerID != suggestion.PlayerID)
                {
                    Database.DataModels.Player nextResponder = db.Players.Where(x => x.ID == nextPlayerID).FirstOrDefault();
                    nextResponder.IsCurrentRespondingPlayer = true;
                    db.SaveChanges();
                }
            }

            return responseClue;
        }

        private static int FindNextResponder (int suggestionID)
        {
            ClueLessContext db = new ClueLessContext();
            Database.DataModels.Suggestion suggestion = db.Suggestions.Where(x => x.ID == suggestionID).FirstOrDefault() ;
            Database.DataModels.Player author=db.Players.Where(x=>x.ID==suggestion.PlayerID).FirstOrDefault();
            int nextPlayerID;
            //Pull the list of all the players
            List<Database.DataModels.Player> players = db.Players.Where(x => x.GameID == author.GameID).OrderBy(x => x.ID).ToList();

            //Get the index of the current player
            int currentIndex = players.IndexOf(players.Where(x => x.IsCurrentRespondingPlayer).FirstOrDefault());

            if (currentIndex == players.Count - 1)
            {
                nextPlayerID = players.ElementAt(0).ID;
            }
            else
            {
                nextPlayerID = players.ElementAt(currentIndex + 1).ID;
            }

            return nextPlayerID;
        }

        public  int ID { get; set; }
        public int playerID { get; set; }
        public int characterID { get; set; }
        public int LocationID { get; set; }
        public int weaponID { get; set; }
        public  string PlayerName { get; set; }
        public  string Location { get; set; }
        public  string Weapon { get; set; }
        public  string Character { get; set; }
        public  bool ClueRevealed { get; set; }
        public  int ClueRevealedBy { get; set; }
        public  bool IsAccusastion { get; set; }
    }
}