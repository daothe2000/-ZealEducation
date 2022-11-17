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
    public class SubjectsController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        // GET: Subjects
        public ActionResult Index(string search_name)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            search_name = search_name ?? "";
            var sub = db.Subjects.Where(s => s.SubjectName.Contains(search_name)).Include(x => x.Faculty);
            ViewBag.sub = new SelectList(db.Faculties, "FacultyId", "FacultyName");
            return View(sub.ToList());
        }


        // GET: Subjects/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subjectTable = db.Subjects.Find(id);
            if (subjectTable == null)
            {
                return HttpNotFound();
            }
            return View(subjectTable);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyID", "FacultyName");
            return View();
        }

        // 1: tạo ra một phương thức get theo id của bản ghi click vào

        //public SubjectTable GetById(int id)
        //{
        //    // làm giống như lúc lấy ra ất cả bản ghi chỉ thêm một điều kiện là trùng với id gửi lên=> sau đấy trả dữ liệu về view edit
        //    SubjectTable subjectTable = db.Subjects.Find(id);
        //    return subjectTable;
        //}
        [HttpGet]
        public JsonResult getByIdToJson(string id)
        {
            if (id != null)
            {
                Subject st = db.Subjects.Find(id);
                return Json(new { SubjectID = st.SubjectID, SubjectName = st.SubjectName, abbreviation = st.Abbreviation, price = st.price, lesson = st.lesson, FacultyId = st.FacultyID }, JsonRequestBehavior.AllowGet);
            }
            return Json("Fail", JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditData(Subject sub)
        {
            var data = db.Subjects.Find(sub.SubjectID);
            data.SubjectName = sub.SubjectName;
            data.price = sub.price;
            data.Abbreviation = sub.Abbreviation;
            data.lesson = sub.lesson;
            data.Status = sub.Status;
            data.FacultyID = sub.FacultyID;
            db.SaveChanges();
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectID,SubjectName,abbreviation,lesson,price,status,FacultyId")] Subject subjectTable)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subjectTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyID", "FacultyName", subjectTable.FacultyID);
            return View(subjectTable);
        }


        public JsonResult CreateData(Subject subjectTable)
        {
            var data = db.Subjects.Add(subjectTable);
            db.SaveChanges();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subjectTable = db.Subjects.Find(id);
            if (subjectTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyID = new SelectList(db.Faculties, "FacultyID", "FacultyName", subjectTable.FacultyID);
            return View(subjectTable);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectID,SubjectName,abbreviation,lesson,price,status,FacultyId")] Subject subjectTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyId = new SelectList(db.Faculties, "FacultyID", "FacultyName", subjectTable.FacultyID);
            return View(subjectTable);
        }



        // GET: Subjects/Delete/5
        public ActionResult Delete(string id)
        {
            
            try
            {
                Subject subjectTable = db.Subjects.Single(x => x.SubjectID.Equals(id));
                db.Subjects.Remove(subjectTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "delete failed";
                return RedirectToAction("Index");
            }
        }

        // POST: Subjects/Delete/5
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
