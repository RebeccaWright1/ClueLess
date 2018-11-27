using ClueLess.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace ClueLess.Controllers
{

    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

      [HttpGet]
        public String HelloWorld()
        {
            return "Hello World";
        }

        [HttpGet]
        public Account GetAccountInformation(int userID)
        {
            Account userAccount = Account.GetAccount(userID);
            return userAccount;
        }

        [HttpPost]
        public void SetAccountInformation(string userInfo)
        {
            Account userInfoRecieved = JsonConvert.DeserializeObject<Account>(userInfo);
            Account.SetAccount(userInfoRecieved);
        }
    }
}
