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
            return Ok();
        }

        public IHttpActionResult SignIn(String username, String password)
        {
            return Ok();
        }

        public IHttpActionResult SignOut()
        {
            return Ok();
        }

        public IHttpActionResult ResetPassword(int userID, string newPassword)
        {
            return Ok();
        }

        public IHttpActionResult ForgotUsername(string emailAddresss)
        {
            return Ok();
        }


        public Account EditAccount(int userID)
        {
            Account newAccount = new Account();
            return newAccount;
        }
    }
}
