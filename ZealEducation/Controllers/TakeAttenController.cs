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
    public class TakeAttenController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        // GET: TakeAtten
        public ActionResult Index()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Teacher") && string.IsNullOrEmpty(Convert.ToString(Session["TeacherID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            String current = DateTime.Now.ToShortDateString();
            DateTime currentDate = Convert.ToDateTime(current);
            String after7 = DateTime.Now.AddDays(7).ToShortDateString();
            DateTime DateAfter7 = Convert.ToDateTime(after7);
            String TeaId = Session["TeacherID"].ToString();
            String TeaName = db.Teachers.Where(t => t.TeacherID == TeaId).FirstOrDefault().TeacherName;
            var timeTables = db.TimeTables.Where(t => t.TeacherID == TeaId && t.Day == currentDate).Include(t => t.Class).Include(t => t.Room).Include(t => t.Subject);
            ViewBag.timeTablesWeek = db.TimeTables.Where(t => t.TeacherID == TeaId && t.Day > currentDate && t.Day < DateAfter7).Include(t => t.Class).Include(t => t.Room).Include(t => t.Subject);
            return View(timeTables.ToList());
        }


        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TakeAttendance(int? timeID)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Teacher") && string.IsNullOrEmpty(Convert.ToString(Session["TeacherID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            if (timeID == null)
            {
                return RedirectToAction("Index");
            }
            List<Attendance> attenList = db.Attendances.Where(x => x.TimeID == timeID).ToList();
            if (attenList.Count > 0)
            {
                var classID = db.TimeTables.FirstOrDefault(x => x.TimeID == timeID).ClassID;
                var className = db.Classes.FirstOrDefault(x => x.ClassID == classID).ClassName;
                TempData["TimeID"] = timeID;
                TempData["ClassID"] = classID;
                TempData["ClassName"] = className;
                return RedirectToAction("TakeUpdateAttendance", new { timeID = timeID });
            }
            else {
                ViewBag.AttendanceTypeID = new SelectList(db.AttendanceTypes, "AttendanceTypeID", "AttendanceTypeName");
                var classID = db.TimeTables.FirstOrDefault(x => x.TimeID == timeID).ClassID;
                var className = db.Classes.FirstOrDefault(x => x.ClassID == classID).ClassName;
                List<Student> StudentList = db.ClassMembers.Where(x => x.ClassID == classID).Select(y => y.Student).ToList();
                TempData["TimeID"] = timeID;
                TempData["ClassID"] = classID;
                TempData["ClassName"] = className;
                return View(StudentList);
            }
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TakeUpdateAttendance(int? timeID)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Teacher") && string.IsNullOrEmpty(Convert.ToString(Session["TeacherID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            if (timeID == null)
            {
                return RedirectToAction("Index");
            }
            var timeTable = db.TimeTables.FirstOrDefault(x => x.TimeID == timeID);
            DateTime currentDate = DateTime.Now;
            TimeSpan currentTime = currentDate.TimeOfDay;
            TimeSpan EndTime = new TimeSpan(19, 0, 0);
            int result = DateTime.Compare(currentDate, timeTable.Day);
            if (result >= 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AttendanceCodeId = db.AttendanceTypes.Select(m => new SelectListItem { Text = m.AttendanceTypeName, Value = m.AttendanceTypeID.ToString() });
            ViewBag.AttendanceTypeID = new SelectList(db.AttendanceTypes, "AttendanceTypeID", "AttendanceTypeName");
            List<Attendance> AttenList = db.Attendances.Where(x => x.TimeID == timeID).ToList();
            return View(AttenList);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult TakeUpdateAttendance(int? timeID, int[] AttendanceID, string[] StudentID, int[] AttendanceTypeID, string[] Note)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Teacher") && string.IsNullOrEmpty(Convert.ToString(Session["TeacherID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            if (AttendanceID != null && AttendanceTypeID != null && StudentID != null)
            {
                for (int i = 0; i < StudentID.Length; i++)
                {
                    Attendance atten = db.Attendances.Find(Convert.ToInt32(AttendanceID[i]));
                    atten.AttendanceTypeID = AttendanceTypeID[i];
                    atten.Note = Note[i];
                    db.SaveChanges();
                }
                TempData.Keep();
                return RedirectToAction("TakeUpdateAttendance", new { timeID = timeID });
            }
            else
            {
                return RedirectToAction("Index", "TakeAtten");
            }
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult SubmitAttendance(int? timeID ,string[] StudentID, int[] AttendanceTypeID ,string[] Note)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Teacher") && string.IsNullOrEmpty(Convert.ToString(Session["TeacherID"])))
            {
                return RedirectToAction("Index", "Home");
            }
            if (AttendanceTypeID != null && StudentID != null)
            {
                for (int i = 0; i < StudentID.Length; i++)
                {
                    Attendance atten = new Attendance();
                    atten.StudentID = StudentID[i];
                    atten.TimeID = Int32.Parse(TempData["TimeID"].ToString());
                    atten.AttendanceTypeID = AttendanceTypeID[i];
                    atten.Note = Note[i];
                    atten.timeIn = DateTime.Now.TimeOfDay;
                    db.Attendances.Add(atten);
                    db.SaveChanges();
                }
                TimeTable tTable = db.TimeTables.Find(Convert.ToInt32(TempData["TimeID"].ToString()));
                tTable.Isfinished = true;
                db.SaveChanges();
                TempData.Keep();
                return RedirectToAction("TakeUpdateAttendance", new { timeID = timeID });
            }
            else
            {
                return RedirectToAction("Index", "TakeAtten");
            }
        }


        // GET: TakeAtten/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: TakeAtten/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            ViewBag.TimeID = new SelectList(db.TimeTables, "TimeID", "ClassID");
            ViewBag.AttendanceTypeID = new SelectList(db.AttendanceTypes, "AttendanceTypeID", "AttendanceTypeName");
            return View();
        }

        // POST: TakeAtten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendanceID,StudentID,TimeID,AttendanceTypeID,Note,timeIn")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", attendance.StudentID);
            ViewBag.TimeID = new SelectList(db.TimeTables, "TimeID", "ClassID", attendance.TimeID);
            ViewBag.AttendanceTypeID = new SelectList(db.AttendanceTypes, "AttendanceTypeID", "AttendanceTypeName", attendance.AttendanceTypeID);
            return View(attendance);
        }

        // GET: TakeAtten/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", attendance.StudentID);
            ViewBag.TimeID = new SelectList(db.TimeTables, "TimeID", "ClassID", attendance.TimeID);
            ViewBag.AttendanceTypeID = new SelectList(db.AttendanceTypes, "AttendanceTypeID", "AttendanceTypeName", attendance.AttendanceTypeID);
            return View(attendance);
        }

        // POST: TakeAtten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendanceID,StudentID,TimeID,AttendanceTypeID,Note,timeIn")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", attendance.StudentID);
            ViewBag.TimeID = new SelectList(db.TimeTables, "TimeID", "ClassID", attendance.TimeID);
            ViewBag.AttendanceTypeID = new SelectList(db.AttendanceTypes, "AttendanceTypeID", "AttendanceTypeName", attendance.AttendanceTypeID);
            return View(attendance);
        }

        // GET: TakeAtten/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: TakeAtten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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
