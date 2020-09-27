using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormAuthentication.Models;
using System.Web.Security;

namespace FormAuthentication.Models
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new FormAuthEntities())
            {
                //check whether username and password are found or not
                bool isValid = context.Users.Any(x => x.Name == model.Name && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, false);
                    return RedirectToAction("Index", "Employees");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username & Password");
                    return View();
                }
            }

        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (var context = new FormAuthEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("login");

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        public ActionResult About()
        {
            return View();
        }

    }
}
