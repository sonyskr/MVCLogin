using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLogin.Models;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(MVCLogin.Models.User userModel)
        {
            using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if(userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userId"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Create()
        {
            return View();
        }


        public ActionResult Logout()
        {
            int userId = (int) Session["userId"];

            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}