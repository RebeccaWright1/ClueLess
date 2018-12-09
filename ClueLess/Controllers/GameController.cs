using ClueLess.Helpers;
using ClueLess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ClueLess.Controllers
{
    public class GameController : ApiController
    {
        public IHttpActionResult SetupBoard(int userID, int configurationID)
        {
            return Ok();
        }

        public IHttpActionResult JoinGame(int userID, int gameID)
        {
            return Ok();
        }

        public IHttpActionResult StartGame(int userID, int gameID)
        {
           
            return Ok();
        }

        public IHttpActionResult ChooseCharacter(int gameID, int characterID, int userID)
        {
            return Ok();
        }

        public IHttpActionResult MakeSuggestion(Suggestion suggestion)
        {
            return Ok();
        }

        public IHttpActionResult RespondToASuggestion (int playerID, int clueID = -1)
        {
            return Ok();
        }

        public IHttpActionResult MoveCharacter(int playerID, int locationID)
        {
            return Ok();
        }

        public IHttpActionResult MakeAccusation(Suggestion accusation)
        {
            return Ok();
        }

        public IHttpActionResult InvitePlayer(String emailAddresses)
        {
            return Ok();
        }

        public IHttpActionResult ShowGameBoard(int gameID)
        {
            return Ok(new Game());
        }

        
        public IHttpActionResult CreateConfiguration()
        {
            Models.Configuration.CreateConfiguration();
            return Ok();
        }
        
        [SwaggerParameter("ConfigurationTest.csv", "Gameboard Configuration", Required=true, Type="file")]
        public IHttpActionResult SaveConfiguration()
        {
            bool configurationSaved;
            //Check the number of files in the current HttpContext
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                HttpPostedFile fileToRead = HttpContext.Current.Request.Files[0];
                configurationSaved= Models.Configuration.SaveConfiguration(fileToRead, 1);
            }
            return Ok();
        }

        public void SendEmail()
        {
            Notification.SendEmail(Notification.NotificationType.UserNameReminder, "crochet.wright@gmail.com", 1);
           
        }

    }
}
