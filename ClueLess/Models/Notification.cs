using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using ClueLess.Database;

namespace ClueLess.Models
{
    public class Notification
    {
        public static void SendEmail( NotificationType notification, string addresses, int userID=-1, int suggestionID= -1, int gameID=-1)
        {
            try
            {
                MailMessage email = new MailMessage("crochet.wright@gmail.com", addresses);
                SmtpClient smtpClient = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                };

                email.Subject = GetSubject(notification);
                email.Body = GetEmailBody(notification, userID, suggestionID, gameID);
                smtpClient.Send(email);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error sending email");
            }
          
            //throw new NotImplementedException();
        }

        private static string GetEmailBody(NotificationType notification, int userID=-1, int suggestionID=-1, int gameID=-1)
        {
            string message = "";
            string opening = "Dear User,";
            string closingStatetment = "This is an automated email, responses sent to this address will not be recieved.\n\n"+   
                "Sincerely,\nIconic Alliance";
            string playerName = "";
            string murderer = "";
            string location = "";
            string weapon = "";
            try
            {
                if (suggestionID > 0)
                {
                    using (ClueLessContext db = new ClueLessContext())
                    {
                        playerName = db.Users.Where(x => x.ID == userID).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
                        //Get the suggestion
                        Database.DataModels.Suggestion suggestion = db.Suggestions.Where(x => x.ID == suggestionID).FirstOrDefault();

                        murderer = suggestion.Character.Character.Name;
                        location = suggestion.Location.Location.LocationName;
                        weapon = suggestion.Weapon.Weapon.Name;

                    }
                }
                if (userID > 0)
                {
                    using (ClueLessContext db = new ClueLessContext())
                    {
                        playerName = db.Users.Where(x => x.ID == userID).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
                    }
                }
                switch (notification)
                {
                    case NotificationType.UserNameReminder:
                        string Username = "";
                        using(ClueLessContext db = new ClueLessContext())
                        {
                            Username = db.Users.Where(x => x.ID == userID).Select(x => x.Username).FirstOrDefault();
                        }
                        message = "Your username for ClueLess is: " + Username + ". If you did not request this information, please check your account.";
                        break;
                    case NotificationType.PasswordReset:
                        message = "Your password has been successfully reset. If you did not request a password reset, please check your account.";
                        break;
                    case NotificationType.GameInvite:
                        message = "You have been invited by " + playerName + " to join them in a game of ClueLess. To accept the invitation, sign in to your ClueLess account. If you do not have an account and wish to join the game, create an account and search for the game to join.";
                        break;
                    case NotificationType.NewSuggestion:
                        message = String.Format("A new suggestion has been made by: {0}. {0} suggested that is was {1} in the {2} with the {3}.", playerName, murderer,location,weapon);
                        break;
                    case NotificationType.SuggestionResponse:
                        message = String.Format("{0} has responded to the suggestion: {1} in the {2} with the {3}. To see thier response, sign in to your ClueLess account and re-join your game.", playerName, murderer, location, weapon);
                        break;
                    case NotificationType.AccusationMade:
                        message = String.Format("{0} has accused {1} of killing Mr.Boddy in the {2} with the {3}.", playerName, murderer, location,weapon);
                        break;
                    case NotificationType.GameOver:
                       
                        using( ClueLessContext db=new ClueLessContext())
                        {
                            Database.DataModels.Game game = db.Games.Where(x => x.ID == gameID).FirstOrDefault();
                            if (game != null)
                            {
                                if (game.Status == Database.DataModels.Status.Completed_Solved)
                                    message = String.Format(" Your ClueLess Game {0} has completed. The murderer was commited by {1} in the {2} with the {3}.", game.Name, murderer, location, weapon);

                                if(game.Status==Database.DataModels.Status.Completed_Unsolved)
                                    message = String.Format(" Your ClueLess Game {0} has completed. The murder was unsolved.", game.Name);
                            }
                        }                       

                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error creating email message");
            }
           

            string body = opening + Environment.NewLine+ message + Environment.NewLine + Environment.NewLine + closingStatetment;
            return body;
        }
      
        private static string GetSubject(NotificationType notification)
        {
            string subject = "";
            switch (notification)
            {
                case NotificationType.UserNameReminder:
                    subject = "ClueLess - Username Reminder";
                    break;
                case NotificationType.PasswordReset:
                    subject = " ClueLess - Password Reset";
                    break;
                case NotificationType.GameInvite:
                    subject = "You've been invited to play ClueLess";
                    break;
                case NotificationType.NewSuggestion:
                    subject = "ClueLess - New Suggestion";
                    break;
                case NotificationType.SuggestionResponse:
                    subject = "ClueLess - Suggestion Response has been made";
                    break;
                case NotificationType.AccusationMade:
                    subject = "ClueLess - Accusation Made";
                    break;
                case NotificationType.GameOver:
                    subject = "ClueLess Game Completed";
                    break;
            }

            return subject;
        }

        public enum NotificationType
        {
            UserNameReminder=0,
            PasswordReset=1,
            GameInvite=2,
            NewSuggestion=3,
            SuggestionResponse=4,
            AccusationMade=5,
            GameOver=6

        }
    }
}