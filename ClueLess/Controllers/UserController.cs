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
        [HttpPost, HttpGet]
        [Route("UserController/CreateAccount/")]
        public IHttpActionResult CreateAccount(Account userAccount)
        {
            try
            {
                Account.SetAccount(userAccount);
                return Ok();

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.BadRequest, "Unable to save account");
            }

        }

        [HttpPost]
        [Route("UserController/SignIn/")]
        public IHttpActionResult SignIn(string username, string password)
        {
            //Add code that starts the users session if validated
            int id = Account.ValidateUser(username, password);
            if (id > 0)
            {
                //HttpContext.Current.Session["userID"] = id;
                return Ok("success");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("UserController/SignOut/")]
        public IHttpActionResult SignOut()
        {
            //Add code that ends users session
            //HttpContext.Current.Session.Abandon();
            return Ok();
        }

        [HttpPost]
        [Route("UserController/ResetPassword/")]
        public IHttpActionResult ResetPassword(string username, string oldPassword, string newPassword)
        {
            int userID = Account.ValidateUser(username, oldPassword);
            if (userID > 0)
            {
                Account.ResetPassword(userID, newPassword);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IHttpActionResult ForgotUsername(string emailAddresss)
        {
            Account.RequestUserName(emailAddresss);
            return Ok();
        }

        //[HttpPost]
        //public Account EditAccount(int userID)
        //{
        //    Account newAccount = new Account();
        //    return newAccount;
        //}
    }
}
