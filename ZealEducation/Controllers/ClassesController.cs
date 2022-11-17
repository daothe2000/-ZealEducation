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
    public class ClassesController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        // GET: Classes
        public ActionResult Index(string search_name)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            search_name = search_name ?? "";
            var Classs = db.Classes.Where(x => x.ClassName.Contains(search_name)).Include(c => c.Faculty);
            ViewBag.sub = new SelectList(db.Faculties, "FacultyId", "FacultyName");
            return View(Classs);
        }

        // GET: Classs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class Class = db.Classes.Find(id);
            if (Class == null)
            {
                return HttpNotFound();
            }
            return View(Class);
        }

        // GET: Classs/Create
        public ActionResult Create()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FacultyName");
            return View();
        }
        public List<SelectListItem> getSttus()
        {
            var sttus = new[] {
                new SelectListItem{ Value = "0", Text = "Please Select Gender *" },
                new SelectListItem{ Value = "1", Text = "Đang hoạt động" },
                new SelectListItem{ Value = "2", Text = "Vô hiệu hóa"},
            };
            return sttus.ToList();
        }

        // POST: Classs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,ClassName,status,FacultyID,quantity")] Class Class)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Classes.Add(Class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FacultyName", Class.FacultyID);
            return View(Class);
        }

        [HttpGet]
        public JsonResult getByIdToJson(string id)
        {

            if (id != null)
            {
                Class cl = db.Classes.Find(id);
                return Json(new { ClassID = cl.ClassID, ClassName = cl.ClassName, StuQuantityMax = cl.StuQuantityMax, status = cl.Status, FacultyId = cl.FacultyID }, JsonRequestBehavior.AllowGet);
            }
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditData(Class cla)
        {
            var data = db.Classes.Find(cla.ClassID);
            data.ClassName = cla.ClassName;
            data.StuQuantityMax = cla.StuQuantityMax;
            data.Status = cla.Status;
            data.FacultyID = cla.FacultyID;
            db.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateData(Class Class)
        {
            var data = db.Classes.Add(Class);
            db.SaveChanges();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Classs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class Class = db.Classes.Find(id);
            var chk_status = 0;
            if (Class.Status == true)
            {
                chk_status = 1;
            }
            else
            {
                chk_status = 2;
            }
            ViewBag.getStatus = new SelectList(getSttus(), "Value", "Text", chk_status);
            if (Class == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FacultyName", Class.FacultyID);
            return View(Class);
        }

        // POST: Classs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,ClassName,status,FacultyID,quantity")] Class Class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FacultyName", Class.FacultyID);
            return View(Class);
        }

        // GET: Classs/Delete/5
        public ActionResult Delete(string id)
        {
           
            try
            {
                Class Class = db.Classes.Single(c => c.ClassID.Equals(id));
                db.Classes.Remove(Class);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                TempData["error"] = "delete failed";
                return RedirectToAction("Index");
            }
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
