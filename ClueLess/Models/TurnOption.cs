namespace ClueLess.Models
{
    public class TurnOption
    {
        public static TurnOption TurnOptionFromAction(Database.DataModels.Actions action)
        {
            return new TurnOption
            {
                ID = action.ID,
                Option = action.ActionName
            };
        }
        public  int ID { get; set; }
        public   string Option { get; set; }
    }
}