using System.Data;
using System.Data.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using System.Net;

namespace OnlineMallManagement.Areas.Admin.Controllers
{
    public class TeachersController : Controller
    {
        private ZealEducationMgtDbEntities dbContext = new ZealEducationMgtDbEntities();
        // GET: Admin/Shop_Product


        public ActionResult Index(string name, string FacultyID)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Faculties = dbContext.Faculties.ToList();
            var Teachers = dbContext.Teachers.Include(x => x.Faculty);
            if (!String.IsNullOrEmpty(FacultyID))
            {
                Teachers = Teachers.Where(s => s.FacultyID.Equals(FacultyID));
            }
            if (!String.IsNullOrEmpty(name))
            {
                Teachers = Teachers.Where(s => s.TeacherName.Contains(name) || s.Address.Contains(name)|| s.Phone.Contains(name));
            }

            if (FacultyID == "" && name == "")
            {
                return RedirectToAction("Index");
            }
            ViewBag.name = name;
            return View(Teachers);
        }


        public ActionResult Create()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.FacultyID = new SelectList(dbContext.Faculties.ToList(), "FacultyID", "FacultyName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Teacher t)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var teachers = dbContext.Teachers.Where(x => x.Phone.Equals(t.Phone)).Count();
            var teachers1 = dbContext.Teachers.Where(x => x.Email.Equals(t.Email)).Count();
            var teachers2 = dbContext.Teachers.Where(x => x.TeacherID.Equals(t.TeacherID)).Count();
            try
            {
                ViewBag.FacultyID = new SelectList(dbContext.Faculties.ToList(), "FacultyID", "FacultyName");
                if (ModelState.IsValid)
                {
                    if (teachers == 0 && teachers1 == 0 && teachers2 == 0)
                    {

                        t.Password = new AccountModel().MD5Hash(t.Password);
                        t.Status = true;
                        dbContext.Teachers.Add(t);
                        dbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (teachers == 0 && teachers1 != 0 && teachers2 != 0)
                    {
                        ModelState.AddModelError("TeacherID", "TeacherID already exists!");
                        ModelState.AddModelError("Email", "Email already exists!");

                    }
                    else if (teachers != 0 && teachers1 == 0 && teachers2 != 0)
                    {
                        ModelState.AddModelError("TeacherID", "TeacherID already exists!");
                        ModelState.AddModelError("Phone", "Email already exists!");

                    }

                    else if (teachers != 0 && teachers1 != 0 && teachers2 == 0)
                    {
                        ModelState.AddModelError("Email", "Email already exists!");
                        ModelState.AddModelError("Phone", "Email already exists!");

                    }

                    else if (teachers != 0 && teachers1 == 0 && teachers2 == 0)
                    {


                        ModelState.AddModelError("Phone", "Phone already exists!");

                    }
                    else if (teachers == 0 && teachers1 == 0 && teachers2 != 0)
                    {

                        ModelState.AddModelError("TeacherID", "TeacherID already exists!");

                    }
                    else if (teachers1 != 0 && teachers == 0 && teachers2 == 0)
                    {
                        ModelState.AddModelError("Email", "Email already exists!");

                    }
                    else
                    {
                        ModelState.AddModelError("TeacherID", "TeacherID already exists!");
                        ModelState.AddModelError("Phone", "Phone already exists!");
                        ModelState.AddModelError("Email", "Email already exists!");
                    }

                }

            }
            catch
            {
                return View(t);
            }
            return View(t);
        }


        public ActionResult Edit(string id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var result = dbContext.Teachers.Single(x => x.TeacherID.Equals(id));
            ViewBag.Faculty_ID = new SelectList(dbContext.Faculties.ToList(), "FacultyID", "FacultyName", result.FacultyID);

            return View(result);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Teacher t)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var teachers = dbContext.Teachers.Where(x => x.Phone.Equals(t.Phone)).Count();
            var teachers11 = dbContext.Teachers.Where(x => x.Email.Equals(t.Email)).Count();
            var teacher = dbContext.Teachers.Find(id);

            try
            {
                ViewBag.Faculty_ID = new SelectList(dbContext.Faculties.ToList(), "FacultyID", "FacultyName", t.FacultyID);
                if (ModelState.IsValid)
                {
                    Teacher teachers1 = dbContext.Teachers.Single(x => x.TeacherID.Equals(id));
                    teachers1.TeacherName = t.TeacherName;
                    teachers1.Address = t.Address;
                    teachers1.Gender = t.Gender;
                    teachers1.Password = new AccountModel().MD5Hash(t.Password);
                    teachers1.Image = t.Image;
                    teachers1.Birthday = t.Birthday;
                    teachers1.Nation = t.Nation;
                    teachers1.Degree = t.Degree;
                    teachers1.FacultyID = t.FacultyID;
                    if (teacher.Phone == t.Phone && teacher.Email == t.Email)
                    {

                        dbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (teacher.Phone == t.Phone && teacher.Email != t.Email)
                    {
                        if (teachers11 == 0)
                        {

                            teachers1.Email = t.Email;
                            dbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "Email already exists!");

                        }
                    }
                    else if (teacher.Phone != t.Phone && teacher.Email == t.Email)
                    {
                        if (teachers == 0)
                        {

                            teachers1.Phone = t.Phone;
                            dbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("Phone", "Phone already exists!");

                        }
                    }
                    else
                    {
                        if (teachers == 0 && teachers11 == 0)
                        {

                            teachers1.Phone = t.Phone;

                            teachers1.Email = t.Email;


                            dbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else if (teachers != 0 && teachers11 == 0)
                        {
                            ModelState.AddModelError("Phone", "Phone already exists!");

                        }
                        else if (teachers11 != 0 && teachers == 0)
                        {
                            ModelState.AddModelError("Email", "Email already exists!");
                        }
                        else
                        {
                            ModelState.AddModelError("Phone", "Phone already exists!");
                            ModelState.AddModelError("Email", "Email already exists!");
                        }
                    }
                }

            }
            catch
            {
                return View(t);
            }
            return View(t);
        }


        public ActionResult Delete(string id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var result = dbContext.Teachers.Single(x => x.TeacherID.Equals(id));

            ViewBag.Faculty_ID = new SelectList(dbContext.Faculties.ToList(), "FacultyID", "FacultyName", result.FacultyID);
            return View(result);
        }
        [HttpPost]
        public JsonResult UpdateStatus(string id, bool status)
        {


            var result = dbContext.Teachers.Single(x => x.TeacherID.Equals(id));

            if (result != null)
            {

                result.Status = status;
                dbContext.Entry(result).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);






        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Teacher t)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Faculty_ID = new SelectList(dbContext.Faculties.ToList(), "FacultyID", "FacultyName", t.FacultyID);
            try
            {
                var result = dbContext.Teachers.Single(x => x.TeacherID.Equals(id));
                dbContext.Teachers.Remove(result);
                dbContext.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                ViewBag.error = "<div class='alert alert-danger'>delete failed</div>";
                return View(t);
            }
        }
    }
}