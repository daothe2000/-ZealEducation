using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZealEducation.Controllers
{
    public class HomeController : Controller
    {
        
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TeacherDashboard()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Teacher") && string.IsNullOrEmpty(Convert.ToString(Session["TeacherID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult StudentDashboard()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Student") && string.IsNullOrEmpty(Convert.ToString(Session["StudentID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}