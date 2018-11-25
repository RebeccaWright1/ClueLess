namespace ClueLess.Models
{
    public class Suggestion
    {
        public static bool ValidateAccusation()
        {
            return false;
        }

        public static void RevealClue(int playerID, int clueID, string ClueTable)
        {

        }


        public  int ID { get; set; }
        public  string PlayerName { get; set; }
        public  string Location { get; set; }
        public  string Weapon { get; set; }
        public  string Character { get; set; }
        public  bool ClueRevealed { get; set; }
        public  int ClueRevealedBy { get; set; }
        public  bool IsAccusastion { get; set; }
    }
}