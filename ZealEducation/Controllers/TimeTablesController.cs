using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;

namespace ZealEducationManagement.Controllers
{
    public class TimeTablesController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        // GET: TimeTables
        public ActionResult Index()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var timeTables = db.TimeTables.Include(t => t.Class).Include(t => t.Room).Include(t => t.Subject).Include(t => t.Teacher);
            return View(timeTables.ToList());
        }

        // GET: TimeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return HttpNotFound();
            }
            return View(timeTable);
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        // GET: TimeTables/Create
        public ActionResult Create()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            //ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName");
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName");
            //ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName");
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName");
            ViewBag.ClassList = new SelectList(GetClassList(), "ClassID", "ClassName");
            return View();
        }

        public List<Class> GetClassList()
        {
            //String current = DateTime.Now.ToShortDateString();
            //DateTime currentDate = Convert.ToDateTime(current);
            var timeTableL = db.TimeTables.Select(y => y.ClassID);
            if (timeTableL == null)
            {

                List<Class> ClassList = db.Classes.Where(c => !timeTableL.Contains(c.ClassID)).ToList();
                return ClassList;
            }
            else
            {
                List<Class> ClassList = db.Classes.ToList();
                return ClassList;
            }
            
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
                //var FacultyID = GetClassList().FirstOrDefault(x => x.ClassID == ClassID).FacultyID;
                //List<Subject> SubjectList = db.Subjects.Where(x => x.FacultyId == FacultyID).ToList();
                List<Subject> SubjectList = db.ClassSubjects.Where(x => x.ClassID == ClassID && x.Status == true).Select(y => y.Subject).ToList();
                return Json(SubjectList, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetTeacherList(string SubjectID)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (SubjectID == "")
            {
                return null;
            }
            else
            {
                List<Teacher> TeacherList = db.MultiSubjectTeachers.Where(x => x.SubjectID == SubjectID).Select(y => y.Teacher).ToList();
                return Json(TeacherList, JsonRequestBehavior.AllowGet);
            }
        }
        // Kiểm tra trùng lặp
        public bool TimeTableCheck(String ClassID, String TeacherID, String SubjectID, TimeSpan TimeIn, TimeSpan TimeOut, DateTime Day, int RoomId)
        {
            List<TimeTable> TimeTableList = db.TimeTables.Where(x => x.Day == Day).ToList();
            bool check = true;
            DateTime currentDate = DateTime.Now;
            TimeSpan currentTime = currentDate.TimeOfDay;
            TimeSpan EndTime = new TimeSpan(19, 0, 0);
            int result = DateTime.Compare(currentDate, Day);
            if (result >= 0 && currentTime > TimeIn && TimeIn < EndTime)
            {
                TempData["error"] = "It's past time to schedule class";
                check = false;
            }
            else
            {
                foreach (var item in TimeTableList)
                {
                    if (ClassID == item.ClassID)
                    {
                        TempData["error"] = "A class can only take one lesson in a day";
                        check = false; 
                        break;
                    }
                    else
                    {
                        if (TimeIn < item.TimeIn && TimeOut > item.TimeOut || TimeOut > item.TimeIn && TimeOut <= item.TimeOut || TimeIn >= item.TimeIn && TimeOut < item.TimeOut)
                        {
                            if (RoomId == item.RoomId)
                            {
                                TempData["error"] = "This Room has been used for this time";
                                check = false;
                                break;
                            }
                            else if (TeacherID == item.TeacherID)
                            {
                                TempData["error"] = "This Teacher has been scheduled for this time";
                                check = false;
                                break;
                            }
                            else
                            {

                                check = true;
                                break;
                            }
                        }
                        else
                        {
                            check = true;
                            break;
                        }
                    }
                }
            }

            return check;
        }



        // POST: TimeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TimeID,ClassID,TeacherID,SubjectID,TimeIn,TimeOut,Day,RoomId,IsAcvite,Isfinished")] TimeTable timeTable)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                if (TimeTableCheck(timeTable.ClassID, timeTable.TeacherID, timeTable.SubjectID, timeTable.TimeIn, timeTable.TimeOut, timeTable.Day, timeTable.RoomId))
                {
                    timeTable.IsAcvite = true;
                    timeTable.Isfinished = false;
                    db.TimeTables.Add(timeTable);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ClassList = new SelectList(GetClassList(), "ClassID", "ClassName");
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", timeTable.RoomId);
                    //ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", timeTable.SubjectID);
                    //ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName", timeTable.TeacherID);
                    return View(timeTable);
                }
            }
            else
            {
                TempData["error"] = "validationsummary field is required";
                ViewBag.ClassList = new SelectList(GetClassList(), "ClassID", "ClassName");
                ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", timeTable.RoomId);
                //ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", timeTable.SubjectID);
                //ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName", timeTable.TeacherID);
                return View(timeTable);
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        // GET: TimeTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName", timeTable.ClassID);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", timeTable.RoomId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", timeTable.SubjectID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName", timeTable.TeacherID);
            return View(timeTable);
        }

        // POST: TimeTables1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeID,ClassID,TeacherID,SubjectID,TimeIn,TimeOut,Day,RoomId,IsAcvite,Isfinished")] TimeTable timeTable)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                db.Entry(timeTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName", timeTable.ClassID);
            ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", timeTable.RoomId);
            ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName", timeTable.SubjectID);
            ViewBag.TeacherID = new SelectList(db.Teachers, "TeacherID", "TeacherName", timeTable.TeacherID);
            return View(timeTable);
        }

        // GET: TimeTables/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeTable timeTable = db.TimeTables.Find(id);
            if (timeTable == null)
            {
                return HttpNotFound();
            }
            return View(timeTable);
        }

        // POST: TimeTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TimeTable timeTable = db.TimeTables.Find(id);
            db.TimeTables.Remove(timeTable);
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
