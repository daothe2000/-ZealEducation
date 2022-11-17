using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace ZealEducation.Controllers
{
    public class ClassMembersController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: ClassMembers
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(string search_class, string search_student)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var student = db.ClassMembers.ToList();
            if (!String.IsNullOrEmpty(search_class))
            {
                student = student.Where(s => s.Class.ClassName.Contains(search_class)).ToList();
            }
            if (!String.IsNullOrEmpty(search_student))
            {
                student = student.Where(s => s.Student.StudentName.Contains(search_student)).ToList();
            }
            return View(student);
        }
        public ActionResult Create()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName");
            //ViewBag.StudentID = new SelectList(db.Student, "StudentID", "StudentName");
            var ClasssMemberList = from c in db.ClassMembers select c.StudentID;
            var item = db.Students.Where(s => !ClasssMemberList.Contains(s.StudentID)).ToList();
            //var item = db.Students;
            ViewBag.Student = item;
            return View();
        }

        // POST: ClassMember/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String ClassID, String[] StudentIds) /*([Bind(Include = "ID,ClassID,StudentID")] ClassMemberTable classMemberTable)*/
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            bool check = true;

            var data = db.ClassMembers.ToList();
            //if (StudentIds != null)
            //{

            //    //foreach (var item in data)
            //    //{
            //    //    for (int i = 0; i < StudentIds.Length; i++)
            //    //    {
            //    //        if (item.StudentID == StudentIds[i])
            //    //        {
            //    //            check = false;
            //    //            ViewBag.err = "<div class='alert alert-danger'>Student Id existed</div> ";
            //    //        }
            //    //    }
            //    //}
            //}

            if (check)
            {
                foreach (String stuId in StudentIds)
                {
                    ClassMember stuClass = new ClassMember();
                    stuClass.ClassID = ClassID;
                    stuClass.StudentID = stuId;
                    db.ClassMembers.Add(stuClass);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var del = db.ClassMembers.Find(id);
            if (del != null)
            {
                db.ClassMembers.Remove(del);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
