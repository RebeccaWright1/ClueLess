using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ClueLess.Models
{
    public class LoginDTO
    {
        
        public  String password { get; set; }
        public String username { get; set; }
    }
}