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
    public class ClassSubjectsController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: ClassSubjects
        public ActionResult Index()
        {
            var classSubjects = db.ClassSubjects.Include(c => c.Class).Include(c => c.Subject);
            return View(classSubjects.ToList());
        }

        // GET: ClassSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSubject classSubject = db.ClassSubjects.Find(id);
            if (classSubject == null)
            {
                return HttpNotFound();
            }
            return View(classSubject);
        }

        // GET: ClassSubjects/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName");
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
                var FacultyID = db.Classes.FirstOrDefault(x => x.ClassID == ClassID).FacultyID;
                var ClassSubjectList = from c in db.ClassSubjects where c.ClassID.Contains(ClassID) select c.SubjectID;
                var SubjectList = db.Subjects.Where(s => s.FacultyID == FacultyID && !ClassSubjectList.Contains(s.SubjectID)).ToList();
                return Json(SubjectList, JsonRequestBehavior.AllowGet);
            }
        }

        

        // POST: ClassSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClassID,SubjectID,Status")] ClassSubject classSubject, string[] feestd)
        {

            if (ModelState.IsValid)
            {
                db.ClassSubjects.Add(classSubject);
                var sbj = db.Subjects.Find(classSubject.SubjectID);

                Fee fee = new Fee();
                fee.FeesDate = DateTime.Now;
                fee.TotalPrice = sbj.price;
                fee.Status = true;
                db.Fees.Add(fee);

                FeesDetail fd = new FeesDetail();
                fd.FeesID = fee.FeesID;
                fd.SubjectID = classSubject.SubjectID;
                fd.price = sbj.price;
                db.FeesDetails.Add(fd);

                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName", classSubject.ClassID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", classSubject.SubjectID);
            return View(classSubject);
        }

        // GET: ClassSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassSubject classSubject = db.ClassSubjects.Find(id);
            if (classSubject == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName", classSubject.ClassID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", classSubject.SubjectID);
            return View(classSubject);
        }

        // POST: ClassSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClassID,SubjectID,Status")] ClassSubject classSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName", classSubject.ClassID);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", classSubject.SubjectID);
            return View(classSubject);
        }

        // GET: ClassSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            ClassSubject classSubject = db.ClassSubjects.Find(id);
            db.ClassSubjects.Remove(classSubject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: ClassSubjects/Delete/5
       

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
