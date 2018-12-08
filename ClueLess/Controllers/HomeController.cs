using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClueLess.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Title = "About";
            return View();
        }

        public ActionResult GameView()
        {
            ViewBag.Title = "Game";
            return View();
        }


        public ActionResult UserDashBoard()
        {
            ViewBag.Title = "User DashBoard";
            return View();
        }

        public ActionResult EditProfile()
        {
            ViewBag.Title = "Edit Profile";
            return View();
        }
    }
}
