using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ZealEducationMgt.Controllers
{
    public class ExamScheduleController : Controller
    {
        // GET: ExamSchedule
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: TimeTables
        public ActionResult Index()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var timeTables = db.ExamSchedules.Include(t => t.Class).Include(t => t.Room).Include(t => t.Subject).OrderByDescending(x => x.ExamScheduleID);
            return View(timeTables.ToList());
        }

        // GET: TimeTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            if (examSchedule == null)
            {
                return HttpNotFound();
            }
            return View(examSchedule);
        }

        // GET: TimeTables/Create
        public ActionResult Create()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            //ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "ClassName");
            ViewBag.RoomId = new SelectList(db.Rooms.Where(x => x.Status == true), "RoomID", "RoomName");
            //ViewBag.SubjectID = new SelectList(db.Subjects, "SubjectID", "SubjectName");
           
            ViewBag.ClassList = new SelectList(GetClassList(), "ClassId", "ClassName");
            return View();
        }

        public List<Class> GetClassList()
        {

            List<Class> ClassList = db.Classes.ToList();
            return ClassList;
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
                var ClassSubject = from c in db.ExamSchedules where c.ClassId==ClassID select c.SubjectID;
                List<Subject> SubjectList = db.ClassSubjects.Where(x => x.ClassID == ClassID && x.Status == true && !ClassSubject.Contains(x.SubjectID)).Select(y => y.Subject).ToList();
                return Json(SubjectList, JsonRequestBehavior.AllowGet);
            }
        }


        // Kiểm tra trùng lặp
        public bool TimeTableCheck(String ClassID, String SubjectID, DateTime Examday, TimeSpan TimeIn, TimeSpan TimeOut, String Note, int RoomId, bool status)
        {
            List<ExamSchedule> TimeTableList = db.ExamSchedules.Where(x => x.Examday == Examday).ToList();
            bool check = true;
            DateTime currentDate = DateTime.Now;
            TimeSpan EndTime = new TimeSpan(19, 0, 0);
            int result = DateTime.Compare(currentDate, Examday);
            if (result >= 0 && TimeIn > EndTime)
            {
                TempData["error"] = "It's past time to schedule class";
                check = false;
            }
            else
            {
                foreach (var item in TimeTableList)
                {
                    if (TimeIn < item.TimeIn && TimeOut > item.TimeOut || TimeOut > item.TimeIn && TimeOut <= item.TimeOut || TimeIn >= item.TimeIn && TimeOut < item.TimeOut||TimeIn<item.TimeOut&&TimeOut>item.TimeOut|| TimeIn < item.TimeIn&&TimeOut<item.TimeOut)
                    {
                        if (RoomId == item.RoomID)
                        {
                            TempData["error"] = "This Room has been used for this time";
                            check = false;
                        }

                        else if (ClassID == item.ClassId)
                        {
                            TempData["error"] = "This Class is scheduled for this time";
                            check = false;
                        }
                        else if (ClassID == item.ClassId)
                        {
                            TempData["error"] = "This Class is scheduled for this time";
                            check = false;
                        }
                        else
                        {

                            check = true;
                        }
                    }
                    else
                    {
                        check = true;
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
        public ActionResult Create([Bind(Include = "ExamScheduleID,ClassId,SubjectID,Examday,TimeIn,TimeOut,Note,RoomId,status")] ExamSchedule exam)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var Examsche = db.ExamSchedules.Where(x => x.ClassId.Equals(exam.ClassId) && x.SubjectID.Equals(exam.SubjectID)).Count();
            TimeSpan StartTime = TimeSpan.Parse("02:00:00");
            if (ModelState.IsValid)
            {
                if (Examsche == 0)
                {
                    if (exam.TimeIn < exam.TimeOut)
                    {
                        if (exam.TimeOut - exam.TimeIn >= StartTime)
                        {
                            if (TimeTableCheck(exam.ClassId, exam.SubjectID, exam.Examday, exam.TimeIn, exam.TimeOut, exam.Note, exam.RoomID, exam.status))
                            {
                                exam.status = true;
                                db.ExamSchedules.Add(exam);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.ClassList = new SelectList(GetClassList(), "ClassId", "ClassName");
                                ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);

                                return View(exam);
                            }
                        }
                        else
                        {
                            TempData["error"] = "time must be greater than or equal to 02:00";
                            ViewBag.ClassList = new SelectList(GetClassList(), "ClassId", "ClassName");
                            ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);

                            return View(exam);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("TimeOut", "time end must be bigger than h start!");
                        ViewBag.ClassList = new SelectList(GetClassList(), "ClassId", "ClassName");
                        ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);

                        return View(exam);
                    }
                }
                else
                {
                    TempData["error"] = "This class has an exam schedule for this subject";
                    ViewBag.ClassList = new SelectList(GetClassList(), "ClassId", "ClassName");
                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);

                    return View(exam);
                }

            }
            else
            {
                TempData["error"] = "validationsummary field is required";
                ViewBag.ClassList = new SelectList(GetClassList(), "ClassId", "ClassName");
                ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);


                return View(exam);
            }
        }

        //// GET: TimeTables/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ExamSchedule timeTable = db.ExamSchedules.Find(id);
        //    if (timeTable == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", timeTable.RoomID);
        //    ViewBag.ClassList = new SelectList(db.Classes.ToList(), "ClassId", "ClassName", timeTable.ClassId);
        //    var examsch = from c in db.ExamSchedules where c.ExamScheduleID == id select c.ClassId;

        //    ViewBag.SubjectID = new SelectList(db.ClassSubjects.Where(x => examsch.Contains(x.ClassID) && x.Status == true).Select(y => y.Subject).ToList(), "SubjectID", "SubjectName", timeTable.SubjectID);




        //    return View(timeTable);
        //}


        //// POST: TimeTables/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ExamScheduleID,ClassId,SubjectID,Examday,TimeIn,TimeOut,Note,RoomId")] ExamSchedule exam)
        //{
        //    var Examsche = db.ExamSchedules.Where(x => x.ClassId.Equals(exam.ClassId) && x.SubjectID.Equals(exam.SubjectID)).Count();
        //    TimeSpan StartTime = TimeSpan.Parse("02:00:00");
        //    if (ModelState.IsValid)
        //    {
        //        if (Examsche == 0)
        //        {
        //            if (exam.TimeIn < exam.TimeOut)
        //            {
        //                if (exam.TimeOut - exam.TimeIn >= StartTime)
        //                {

        //                    db.Entry(exam).State = EntityState.Modified;
        //                    db.SaveChanges();
        //                    return RedirectToAction("Index");

        //                }
        //                else
        //                {
        //                    TempData["error"] = "validationsummary field is required";
        //                    ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);
        //                    ViewBag.ClassList = new SelectList(db.Classes.ToList(), "ClassId", "ClassName", exam.ClassId);
        //                    var examsch = from c in db.ExamSchedules where c.ExamScheduleID == exam.ExamScheduleID select c.ClassId;
        //                    ViewBag.SubjectID = new SelectList(db.ClassSubjects.Where(x => examsch.Contains(x.ClassID) && x.Status == true).Select(y => y.Subject).ToList(), "SubjectID", "SubjectName", exam.SubjectID);



        //                    return View(exam);
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("TimeOut", "time end must be bigger than h start!");
        //                ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);
        //                ViewBag.ClassList = new SelectList(db.Classes.ToList(), "ClassId", "ClassName", exam.ClassId);
        //                var examsch = from c in db.ExamSchedules where c.ExamScheduleID == exam.ExamScheduleID select c.ClassId;

        //                ViewBag.SubjectID = new SelectList(db.ClassSubjects.Where(x => examsch.Contains(x.ClassID) && x.Status == true).Select(y => y.Subject).ToList(), "SubjectID", "SubjectName", exam.SubjectID);


        //                return View(exam);
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("TimeOut", "time end must be bigger than h start!");
        //            ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);
        //            ViewBag.ClassList = new SelectList(db.Classes.ToList(), "ClassId", "ClassName", exam.ClassId);
        //            var examsch = from c in db.ExamSchedules where c.ExamScheduleID == exam.ExamScheduleID select c.ClassId;

        //            ViewBag.SubjectID = new SelectList(db.ClassSubjects.Where(x => examsch.Contains(x.ClassID) && x.Status == true).Select(y => y.Subject).ToList(), "SubjectID", "SubjectName", exam.SubjectID);


        //            return View(exam);
        //        }
        //    }
        //    else
        //    {
        //        TempData["error"] = "validationsummary field is required";
        //        ViewBag.RoomId = new SelectList(db.Rooms, "RoomID", "RoomName", exam.RoomID);
        //        ViewBag.ClassList = new SelectList(db.Classes.ToList(), "ClassId", "ClassName", exam.ClassId);
        //        var examsch = from c in db.ExamSchedules where c.ExamScheduleID == exam.ExamScheduleID select c.ClassId;

        //        ViewBag.SubjectID = new SelectList(db.ClassSubjects.Where(x => examsch.Contains(x.ClassID) && x.Status == true).Select(y => y.Subject).ToList(), "SubjectID", "SubjectName", exam.SubjectID);


        //        return View(exam);
        //    }
        //}
        [HttpPost]
        public JsonResult UpdateStatus(int id, bool status)
        {


            var result = db.ExamSchedules.Single(x => x.ExamScheduleID.Equals(id));

                if (result != null)
                {
                    result.status = status;
                    db.Entry(result).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return Json(new { result = false }, JsonRequestBehavior.AllowGet);

           

        }
        // GET: TimeTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var del = db.ExamSchedules.Find(id);
            if (del != null)
            {
                db.ExamSchedules.Remove(del);
                db.SaveChanges();
            }
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