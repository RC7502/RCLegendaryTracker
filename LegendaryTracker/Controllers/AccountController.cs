using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LegendaryTracker.Services;
using Newtonsoft.Json;

namespace LegendaryTracker.Controllers
{
    public class AccountController : Controller
    {
        private AccountService service;

        public AccountController()
        {
            service = new AccountService(new ExcelDAL());
        }

        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginSubmit(string username, string password)
        {

            var user = service.GetAuthenticatedUser(username, password);
            if (user != null)
            {
                var json = JsonConvert.SerializeObject(user);
                var userCookie = new HttpCookie("user", json);
                HttpContext.Response.SetCookie(userCookie);
                return RedirectToAction("Dashboard", "Home");
            }
            return Json(false);
        }

    }
}
