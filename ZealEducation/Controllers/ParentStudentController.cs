using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZealEducationMgt.Controllers
{
    public class ParentStudentController : Controller
    {
        // GET: ParentStudent
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: ClassMembers
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(string search_class)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var student = db.RelParentStudents.ToList();
            if (!String.IsNullOrEmpty(search_class))
            {
                student = student.Where(s => s.Parent.ParentName.Contains(search_class)|| s.Student.StudentName.Contains(search_class)|| s.Parent.Phone.Contains(search_class)|| s.Student.Phone.Contains(search_class)).ToList();
            }
            ViewBag.name = search_class;
            return View(student);
        }
        public ActionResult Create()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.ParentID = new SelectList(db.Parents, "ParentID", "ParentName");
            //ViewBag.StudentID = new SelectList(db.Student, "StudentID", "StudentName");
            var RelParentStudentsList = from c in db.RelParentStudents select c.StudentID;
            var item = db.Students.Where(s => !RelParentStudentsList.Contains(s.StudentID)).ToList();
            //var item = db.Students;
            
            ViewBag.Student = item;
            return View();
        }

        // POST: ClassMember/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int ParentID, String RelationWithStudent,  String[] StudentIds, RelParentStudent relParent) /*([Bind(Include = "ID,ClassID,StudentID")] ClassMemberTable classMemberTable)*/
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }

            var data = db.RelParentStudents.ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    if (StudentIds != null)
                    {
                        foreach (String stuId in StudentIds)
                        {
                            relParent.RelationWithStudent = RelationWithStudent;
                            relParent.ParentID = ParentID;
                            relParent.StudentID = stuId;
                            db.RelParentStudents.Add(relParent);
                            db.SaveChanges();
                        }

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.ParentID = new SelectList(db.Parents, "ParentID", "ParentName");
                        //ViewBag.StudentID = new SelectList(db.Student, "StudentID", "StudentName");
                        var RelParentStudentsList = from c in db.RelParentStudents select c.StudentID;
                        var item = db.Students.Where(s => !RelParentStudentsList.Contains(s.StudentID)).ToList();
                        ViewBag.Student = item;
                        TempData["error"] = "validationsummary field is required";
                        return View(relParent);
                    }
                }
                else
                {
                    ViewBag.ParentID = new SelectList(db.Parents, "ParentID", "ParentName");
                    //ViewBag.StudentID = new SelectList(db.Student, "StudentID", "StudentName");
                    var RelParentStudentsList = from c in db.RelParentStudents select c.StudentID;
                    var item = db.Students.Where(s => !RelParentStudentsList.Contains(s.StudentID)).ToList();
                    ViewBag.Student = item;
                   
                    return View(relParent);
                }

            }
            catch
            {
                return View(relParent);
            }

        }

        public ActionResult Delete(int id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var del = db.RelParentStudents.Find(id);
            if (del != null)
            {
                db.RelParentStudents.Remove(del);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}