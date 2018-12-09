using ClueLess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ClueLess.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
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

        [HttpPost]
        [Route("UserController/SignIn")]
        public IHttpActionResult SignIn(LoginDTO login)
        {
            //Add code that starts the users session if validated
            int id = Account.ValidateUser(login.username, login.password);
            if (id > 0)
            {
                HttpContext.Current.Session["userID"] = id;
                return Ok();
            }
            else
            {
                return BadRequest("User Does Not Exist, Please Sign Up First");
            }
        }

        [HttpPost]
        public IHttpActionResult SignOut()
        {
            //Add code that ends users session
            HttpContext.Current.Session.Abandon();
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ResetPassword(int userID, string newPassword)
        {
            Account.ResetPassword(userID, newPassword);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ForgotUsername(string emailAddresss)
        {
            Account.RequestUserName(emailAddresss);
            return Ok();
        }

        [HttpPost]
        public Account EditAccount(int userID)
        {
            Account newAccount = new Account();
            return newAccount;
        }
    }
}
