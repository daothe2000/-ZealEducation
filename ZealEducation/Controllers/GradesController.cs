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
    public class GradesController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: Grades
        public ActionResult Index()
        {
            var grades = db.Grades.Include(g => g.GradeType).Include(g => g.Student).Include(g => g.Subject);
            return View(grades.ToList());
        }

        // GET: Grades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // GET: Grades/Create
        public ActionResult Create()
        {
            ViewBag.ClassId = new SelectList(db.Classes, "ClassID", "ClassName");
            ViewBag.GradeTypeID = new SelectList(db.GradeTypes, "GradeTypeID", "GradeTypeName");
            //ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            //ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName");
            return View();
        }

        public JsonResult GetSubjectList(string ClassID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ClassID == "")
            {
                return null;
            }
            else
            {
                List<Subject> SubjectExam = db.ExamSchedules.Where(x => x.ClassId == ClassID).Select(y => y.Subject).ToList();
                return Json(SubjectExam, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult StudentList(string ClassID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (ClassID == "")
            {
                return null;
            }
            else
            {
                List<Student> StudentList = db.ClassMembers.Where(x => x.ClassID == ClassID).Select(y => y.Student).ToList();
                return Json(StudentList, JsonRequestBehavior.AllowGet);
            }
        }

       // POST: Grades/Create
       // To protect from overposting attacks, enable the specific properties you want to bind to, for 
       // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string SubjectID, string[] StudentID, decimal[] GradeOfNumber, string[] Note)//[Bind(Include = "GradeID,StudentID,SubjectID,ExamNumber,GradeOfNumber,note,GradeTypeID,Status")] Grade grade
        {
            if (GradeOfNumber != null && StudentID != null)
            {
               
                    for (int i = 0; i < StudentID.Length; i++)
                    {
                        Grade grade = new Grade();
                        grade.StudentID = StudentID[i];
                        grade.SubjectID = SubjectID;
                        grade.GradeOfNumber = Decimal.Parse(GradeOfNumber[i].ToString());
                        grade.GradeTypeID = 1;
                        var Grade = db.GradeTypes.FirstOrDefault(x => x.GradeTypeID == 1).GradeFail;
                        if (GradeOfNumber[i] > Grade)
                        {
                            grade.Status = true;
                        }
                        else
                        {
                            grade.Status = false;
                        }
                        grade.ExamNumber = 1;
                        grade.note = Note[i];
                        db.Grades.Add(grade);
                        db.SaveChanges();
                    }
                    TempData.Keep();
                    return RedirectToAction("Index");
                
            }
            else
            {
                return View();
            }
        }

        // GET: Grades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeTypeID = new SelectList(db.GradeTypes, "GradeTypeID", "GradeTypeName", grade.GradeTypeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", grade.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", grade.SubjectID);
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeID,StudentID,SubjectID,ExamNumber,GradeOfNumber,note,GradeTypeID,Status")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeTypeID = new SelectList(db.GradeTypes, "GradeTypeID", "GradeTypeName", grade.GradeTypeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", grade.StudentID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", grade.SubjectID);
            return View(grade);
        }

        // GET: Grades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grade grade = db.Grades.Find(id);
            if (grade == null)
            {
                return HttpNotFound();
            }
            return View(grade);
        }

        // POST: Grades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = db.Grades.Find(id);
            db.Grades.Remove(grade);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
