using ClueLess.Database;
using ClueLess.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClueLess.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult CreateGame()
        {



            
            List<Database.DataModels.Game> gameList = new List<Database.DataModels.Game>();
            using (ClueLessContext db = new ClueLessContext())
            {
                var g = Models.Game.GetGameboard(8);
                string[] keys = Request.Form.AllKeys;

                gameList = db.Games.ToList();
                return Json(new { success = g.Name }, JsonRequestBehavior.AllowGet);
            }

            
            

        }

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

        public ActionResult ResetPassword()
        {
            ViewBag.Title = "Reset Password";
            return View();
        }
    }
}
