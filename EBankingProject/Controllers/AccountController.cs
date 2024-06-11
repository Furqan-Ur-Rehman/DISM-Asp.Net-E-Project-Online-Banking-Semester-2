using EBankingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EBankingProject.Controllers
{
    public class AccountController : Controller
    {
        EbankingEntities12 db = new EbankingEntities12();

        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer user)
        {
            var query = db.Customers.SingleOrDefault(x => x.EmailId == user.EmailId && x.Password == user.Password);
            if (query != null)
            {
                if (query.Role == "V")
                {
                    FormsAuthentication.SetAuthCookie(query.EmailId, false);
                    Session["Uid"] = query.CustId;
                    Session["User"] = query.firstName;
                    Session["Email"] = query.EmailId;
                    return RedirectToAction("Index", "User");
                }
                else if (query.Role == "A")
                {
                    FormsAuthentication.SetAuthCookie(query.EmailId, false);
                    Session["Uid"] = query.CustId;
                    Session["User"] = query.firstName;
                    return RedirectToAction("Index", "Admin");
                }


            }
            else
            {
                TempData["msg"] = "User Email and Password is incorrect!";

            }

            return View();

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}