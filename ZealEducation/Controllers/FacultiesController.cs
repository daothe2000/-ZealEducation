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
    public class FacultiesController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: Faculties
        public ActionResult Index(string search_name)
        {
            search_name = search_name ?? "";
            var fac = db.Faculties.Where(f => f.FacultyName.Contains(search_name));
            return View(fac);
        }

        public JsonResult GetAllData()
        {
            var data = db.Faculties.Select(x => new { x.FacultyID, x.FacultyName }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }



        // GET: FacultyTables/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty facultyTable = db.Faculties.Find(id);
            if (facultyTable == null)
            {
                return HttpNotFound();
            }
            return View(facultyTable);
        }

        // GET: FacultyTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacultyTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacultyID,FacultyName,description")] Faculty facultyTable)
        {
            if (ModelState.IsValid)
            {
                db.Faculties.Add(facultyTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facultyTable);
        }

        public JsonResult CreateData(Faculty facultyTable)
        {
            var data = db.Faculties.Add(facultyTable);
            db.SaveChanges();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditData(Faculty fac)
        {
            var data = db.Faculties.Find(fac.FacultyID);
            data.FacultyName = fac.FacultyName;
            data.description = fac.description;
            db.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getByIdToJson(string id)
        {
            if (id != null)
            {
                Faculty fc = db.Faculties.Find(id);
                return Json(new { FacultyID = fc.FacultyID, FacultyName = fc.FacultyName, description = fc.description }, JsonRequestBehavior.AllowGet);
            }
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }
        // GET: FacultyTables/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faculty facultyTable = db.Faculties.Find(id);
            if (facultyTable == null)
            {
                return HttpNotFound();
            }
            return View(facultyTable);
        }

        // POST: FacultyTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacultyID,FacultyName,description")] Faculty facultyTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facultyTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facultyTable);
        }

        // GET: FacultyTables/Delete/5
        public ActionResult Delete(string id)
        {
            
            try
            {
                var result = db.Faculties.Single(x => x.FacultyID.Equals(id));
                db.Faculties.Remove(result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "delete failed";
                return RedirectToAction("Index");
            }
        }

        // POST: FacultyTables/Delete/5
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
