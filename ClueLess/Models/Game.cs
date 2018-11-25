using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLess.Models
{
    public class Game
    {
        public Game() { }

        private void ChangeGameStatus( int statusIndicator)
        {

        }

        private static List<TurnOption> GetMoveOptions()
        {
            return new List<TurnOption>();
        }

        private static List<LocationOption> GetLocationOptions()
        {
            return new List<LocationOption>();
        }

        public static Game GetGameboard(int gameID)
        {
            Game game = new Game();
            return game;
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