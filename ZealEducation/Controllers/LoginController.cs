using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZealEducation.Models;

namespace ZealEducation.Controllers
{
    public class LoginController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();
        // GET: Login
        private AccountModel accModel = new AccountModel();
        public ActionResult Index()
        {
            if (Session["userType"] == null)
            {
                return View();
            }
            else if (Session["userType"].Equals("Admin"))
            {

                return RedirectToAction("Index", "Home");

            }
            else if (Session["userType"].Equals("Teacher"))
            {

                //Faculty Home Page
                return RedirectToAction("TeacherDashboard", "Home");

            }
            else if (Session["userType"].Equals("Student"))
            {
                //Student Home Page
                return RedirectToAction("Index", "ShowStudentAttendance");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(Login l)
        {
            String usertype = "";
            string userName = "";
            String passwordteaher = "";
            String password = "";
            var passMd5h = accModel.MD5Hash(l.password);
            usertype = l.usertype.ToString();
            userName = l.username;
            password = l.password.ToString();
            passwordteaher = passMd5h;


            if (usertype.Equals("Admin"))
            {
                Staff s = db.Staffs.Where(x => (x.Email == userName || x.StaffID == userName)&& x.Password == password).FirstOrDefault();
                var userId = db.Staffs.Where(x => x.Email == userName).Select(x => x.StaffID).FirstOrDefault();
                if (s == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    Session["userId"] = userId;
                    Session["userName"] = userName;
                    Session["userType"] = usertype;
                    Session["StaffID"] = s.StaffID;
                    Session["Password"] = s.Password;
                    Session["Phone"] = s.Phone;
                    Session["Address"] = s.Address;
                    Session["Gender"] = s.Gender;
                    Session["Email"] = s.Email;
                    Session["Image"] = s.Image;
                    Session["Birthday"] = s.Birthday;
                }
                return RedirectToAction("Index", "Home");
            }
            else if (usertype.Equals("Teacher"))
            {
                
                Teacher tea = db.Teachers.Where(x => (x.Email == userName || x.TeacherID == userName) && x.Password == passMd5h).FirstOrDefault();
                var userId = db.Teachers.Where(x => x.Email == userName).Select(x => x.TeacherID).FirstOrDefault();
                if (tea == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return View("Index");
                }
                else
                {
                    Session["userType"] = usertype;
                    Session["TeacherID"] = tea.TeacherID;
                    Session["TeacherName"] = tea.TeacherName;
                    Session["Phone"] = tea.Phone;
                    Session["Address"] = tea.Address;
                    Session["Gender"] = tea.Gender;
                    Session["Password"] = tea.Password;
                    Session["Email"] = tea.Email;
                    Session["Image"] = tea.Image;
                    Session["Birthday"] = tea.Birthday;
                    Session["Nation"] = tea.Nation;
                    Session["Degree"] = tea.Degree;
                    Session["FacultyID"] = tea.FacultyID;
                    return RedirectToAction("TeacherDashboard", "Home");
                }
            }
            else if (usertype.Equals("Student"))
            {

                Student stu = db.Students.Where(x => x.StudentID == userName && x.Password == password).FirstOrDefault();
                var userId = db.Students.Where(x => x.StudentID == userName).Select(x => x.StudentID).FirstOrDefault();
                if (stu == null)
                {
                    ViewBag.ErrorMessage = "Please provide correct credentials!!";
                    return View("Index");
                }
                else
                {
                    Session["userType"] = usertype;
                    Session["StudentID"] = stu.StudentID;
                    Session["StudentName"] = stu.StudentName;
                    Session["Phone"] = stu.Phone;
                    Session["Address"] = stu.Address;
                    Session["Gender"] = stu.Gender;
                    Session["Password"] = stu.Password;
                    Session["Email"] = stu.Email;
                    Session["Image"] = stu.Image;
                    Session["Birthday"] = stu.Birthday;
                    Session["Educationlevel"] = stu.Educationlevel;
                    return RedirectToAction("Index", "StudentDashboard");
                }
            }

            return View();

        }
        public ActionResult InvalidLogin()
        {
            return View();
        }
    }
}