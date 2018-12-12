using ClueLess.Database;
using ClueLess.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Game
    {
        public Game()
        {
            Random rnd = new Random();
            ID = rnd.Next(1, 100000);
            Name = "Game: " + ID.ToString();
            using(ClueLessContext db=new ClueLessContext())
            {

            }
        }
        public Game(int userID, int configurationID, string name)
        {
            using (ClueLessContext db = new ClueLessContext())
            {
                db.Games.Add(new Database.DataModels.Game
                {
                    UserID = userID,
                    ConfigurationID = configurationID,
                    Name = name
                });
                db.SaveChanges();
            }
        }

        public static void ChangeGameStatus( int gameID, Database.DataModels.Status status)
        {
            using(ClueLessContext db= new ClueLessContext())
            {
                Database.DataModels.Game game = db.Games.Where(x => x.ID == gameID).FirstOrDefault();
                game.Status = status;
                db.SaveChanges();
            }
        }

        public static List<Actions> GetMoveOptions()
        {
            using(ClueLessContext db= new ClueLessContext())
            {
                return db.Actions.ToList();
            }
           
        }

        public static List<LocationOption> GetLocationOptions(int gameID)
        {
            ClueLessContext db = new ClueLessContext();
            //get the game's configuration ID
            int configurationID = db.Games.Where(x => x.ID == gameID).Select(x=>x.ConfigurationID).FirstOrDefault();
            List<LocationOption> locations = new List<LocationOption>();
            List<LocationOption> position = db.Positions.Where(x => x.ConfigurationID == configurationID).Select(x => new LocationOption { ID = x.ID, Location = x.Location.LocationName }).ToList();
            return position;
        }

        public List<Database.DataModels.Game> PullGameList()
        {
            List<Database.DataModels.Game> gameList= new List<Database.DataModels.Game>();
            using(ClueLessContext db= new ClueLessContext())
            {
                gameList = db.Games.ToList();
            }
            return gameList;
        }


        public static Game GetGameboard(int gameID)
        {
            Game game = new Game();
            return game;
        }

        public static void MoveCharacter(int playerID, int locationID)
        {
            using(ClueLessContext db= new ClueLessContext())
            {
                Database.DataModels.Player playerToBeMoved = db.Players.Where(x => x.ID == playerID).FirstOrDefault();
                playerToBeMoved.PositionID = locationID;
                db.SaveChanges();
            }
        }

        public  int ID { get; set; }
        public  String Name { get; set; }
        public  String Status { get; set; }
        public  List<Player> Players { get; set; }
        public  List<BoardLocation> Locations { get; set; }
        public  List<Suggestion> Suggestions { get; set; }
        public  List<TurnOption> TurnOptions { get; set; }
        public  List<LocationOption> LocationOptions { get; set; }
        public  int ConfigurationID { get; set; }

        
    }
}