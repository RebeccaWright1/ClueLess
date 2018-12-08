using ClueLess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClueLess.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult CreateAccount(Account userAccount)
        {
            try
            {
                Account.SetAccount(userAccount);
                return Ok();

            }
            catch(Exception e)
            {
                return Content(HttpStatusCode.BadRequest, "Unable to save account");
            }
            
        }

        public IHttpActionResult SignIn(String username, String password)
        {
            //Add code that starts the users session if validated
            return Ok();
        }

        public IHttpActionResult SignOut()
        {
            //Add code that ends users session
            return Ok();
        }

        public IHttpActionResult ResetPassword(int userID, string newPassword)
        {
            Account.ResetPassword(userID, newPassword);
            return Ok();
        }

        public IHttpActionResult ForgotUsername(string emailAddresss)
        {
            Account.RequestUserName(emailAddresss);
            return Ok();
        }


        public Account EditAccount(int userID)
        {
            Account newAccount = new Account();
            return newAccount;
        }
    }
}
