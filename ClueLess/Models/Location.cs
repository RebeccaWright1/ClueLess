using ClueLess.Database;
using ClueLess.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Location:Clue
    {
        public bool DetermineAvailability(int locationID, int gameID)
        {
            bool isAvailable = true;

            using(ClueLessContext db = new ClueLessContext())
            {
                Position position = db.Positions.Where(x => x.ID == locationID).FirstOrDefault();
                int numOccupants = db.Players.Where(x => x.PositionID == locationID && x.GameID == gameID).Count();
                if (position.Location.LocationType == Database.DataModels.LocationType.Hallway && numOccupants > 0) isAvailable = false;
               
            }

            return isAvailable;
        }

        public Clue RevealClue(int locationID, int gameID, int playerID)
        {
            Clue revealedClue = new Clue();
            using(ClueLessContext db= new ClueLessContext())
            {
                //Get the ClueLocation Object
                PositionToClue clue = db.PositionsToClues.Where(x => x.PositionID == locationID && x.GameID == gameID).FirstOrDefault();
                //Based on the clue type added the clue in the room to the player's  tracking table if it does not already exist
                int clueID=-1;
                switch (clue.ClueType)
                {
                    case "Character":
                        //Look for the clue in the player's tracked character clues
                        clueID = db.PlayersToCharcters.Where(x => x.CharacterConfigurationID == clue.ClueID && x.PlayerID == playerID).Select(x => x.ID).FirstOrDefault();
                        //if it doesn't exist, add it to the player's tracker character clues
                        if (clueID <= 0)
                        {
                            db.PlayersToCharcters.Add(new PlayerToCharacter
                            {
                                PlayerID = playerID,
                                CharacterConfigurationID = clue.ClueID,
                            });
                            db.SaveChanges();
                        }
                        //Pull the information for the clue to be returned
                        CharacterConfiguration characterConfiguration = db.CharacterConfigurations.Where(x => x.ID == clue.ClueID).FirstOrDefault();
                        revealedClue = new Clue
                        {
                            ID=characterConfiguration.ID,
                            Name = characterConfiguration.Character.Name,
                        };

                        break;
                    case "Weapon":
                        //Look for the clue in the player's tracked weapon clues
                        clueID = db.PlayersToWeapons.Where(x => x.WeaponConfigurationID == clue.ClueID && x.PlayerID == playerID).Select(x => x.ID).FirstOrDefault();
                        //if it doesn't exist, add it to the player's tracked weapon clues
                        if (clueID <= 0)
                        {
                            db.PlayersToWeapons.Add(new PlayerToWeapon
                            {
                                PlayerID = playerID,
                                WeaponConfigurationID = clue.ClueID
                            });
                            db.SaveChanges();
                        }

                        //Pull the informaton for the clue to be returned
                        WeaponConfiguration weaponConfiguration = db.WeaponConfigurations.Where(x => x.ID == clue.ClueID).FirstOrDefault();
                        revealedClue = new Clue { ID = weaponConfiguration.ID, Name = weaponConfiguration.Weapon.Name };

                        break;
                    case "Room":
                        clueID = db.PlayersToLocations.Where(x => x.PositionID == clue.ClueID && x.PlayerID == playerID).Select(x => x.ID).FirstOrDefault();
                        if (clueID > 0)
                        {
                            db.PlayersToLocations.Add(new PlayerToLocation
                            {
                                PlayerID = playerID,
                                PositionID = clue.ClueID
                            });
                            db.SaveChanges();
                        }

                        //Pull the information for the clue to be returned
                        Position position = db.Positions.Where(x => x.ID == clue.ClueID).FirstOrDefault();
                        revealedClue = new Clue { ID = position.ID, Name = position.Location.LocationName };
                        break;
                    default:
                        throw new ArgumentException();

                }

                // return the clue object
                return revealedClue;
                
            }
        }

        public static string LocationType { get; set; }
        public  int SecretDoorRoom { get; set; }
        public  int WeaponID { get; set; }
        public  int ClueID { get; set; } //This is the ID of the clue that is in the room; This is only populated with a 2-player version on the game
        public  int RowPosition { get; set; }
        public  int ColumnPosition { get; set; }

        
    }
}