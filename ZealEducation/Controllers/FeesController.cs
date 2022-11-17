using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using ZealEducation.Models.ViewModels;

namespace ZealEducation.Controllers
{
    public class FeesController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: Fees
        public ActionResult Index(string search_id,string search_name)
        {

            var fees = from st in db.Students
                       join cm in db.ClassMembers on st.StudentID equals cm.StudentID
                       join cl in db.Classes on cm.ClassID equals cl.ClassID
                       join cs in db.ClassSubjects on cl.ClassID equals cs.ClassID
                       join sbj in db.Subjects on cs.SubjectID equals sbj.SubjectID
                       join fd in db.FeesDetails on sbj.SubjectID equals fd.SubjectID
                       join f in db.Fees on fd.FeesID equals f.FeesID

                       select new FeeViewModel
                       {
                           StudentID = st.StudentID,
                           StudentName = st.StudentName,
                           Phone = st.Phone,
                           Address = st.Address,
                           Gender = st.Gender,
                           Email = st.Email,
                           ClassID = cl.ClassID,
                           ClassName = cl.ClassName,
                           FeesID = f.FeesID,
                           TotalPrice = f.TotalPrice,
                           Status = f.Status
                       };
            if (!String.IsNullOrEmpty(search_id))
            {
                fees = fees.Where(s => s.StudentID.Contains(search_id));
            }
            if (!String.IsNullOrEmpty(search_name))
            {
                fees = fees.Where(s => s.StudentName.Contains(search_name));
            }

            return View(fees.ToList());
        }

        // GET: Fees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // GET: Fees/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeesID,StudentID,FeesDate,TotalPrice,Status")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Fees.Add(fee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", fee.StudentID);
            return View(fee);
        }

        // GET: Fees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            //ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", fee.StudentID);
            return View(fee);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeesID,StudentID,FeesDate,TotalPrice,Status")] Fee fee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", fee.StudentID);
            return View(fee);
        }

        // GET: Fees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fee fee = db.Fees.Find(id);
            if (fee == null)
            {
                return HttpNotFound();
            }
            return View(fee);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fee fee = db.Fees.Find(id);
            db.Fees.Remove(fee);
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
