using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess;
using ZealEducation.Models;

namespace ZealEducation.Controllers
{
    public class AttendancesController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: Attendances
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.Student).Include(a => a.TimeTable).Include(a => a.AttendanceType);
            return View(attendances.ToList());
        }

        public ActionResult AttendanceTable()
        {
            //Get current month number, you can pass this value to controller ActionMethod also
            int Month = DateTime.Now.Month;

            //Create List
            List<StudentAtt> empWithDate = new List<StudentAtt>();
            using (db)
            {
                var List = db.Attendances.Where(a => a.TimeTable.Day.Month == Month).OrderBy(a => a.TimeTable.Day).ToList(); /*&& a.TimeTable.ClassID == "Class001"*/
                var empatt = new StudentAtt();
                foreach (var emp in List)
                {
                    //create new employee object fo each loop

                    //Get Array list for each emp using it's Id and sort list according to datetime
                    var attendnaceList = List.Where(a => a.StudentID == emp.StudentID).OrderBy(a => a.TimeTable.Day).ToArray();

                    //Get Count
                    var Count = attendnaceList.Count();

                    //Loop through all values and increment it by 2 as we will compare two values in/out
                    for (var i = 0; i < Count; i = i + 2)
                    {
                        empatt = new StudentAtt();
                        //Although no need to check but confirm if both the values are of same date,which we will compare
                        if (true)
                        {
                            empatt.Id = emp.StudentID;
                            empatt.Student_name = emp.Student.StudentName;
                            empatt.Date = attendnaceList[i].TimeTable.Day.Date;
                            empatt.AttendanceTypeID = Convert.ToInt32(emp.AttendanceTypeID);
                            empatt.AttendanceTypeName = emp.AttendanceType.AttendanceTypeName;
                            //inout time
                            empatt.InoutTime = attendnaceList[i].TimeTable.TimeIn + "-" + attendnaceList[i].TimeTable.TimeOut;
                            //to avoid duplicate check if value already exists or not, if not add one
                            if (empWithDate.Where(a => a.Student_name == attendnaceList[i].Student.StudentName && a.Date.Value.Date == attendnaceList[i].TimeTable.Day.Date).Count() == 0)
                            {
                                empWithDate.Add(empatt);
                            }
                        }
                    }
                }

                // get emp Name, Id, Date time and order it in ascending order of date
                //empWithDate = db.Attendances.Where(a => a.TimeTable.Day.Month == Month).GroupBy(a => new { Date = System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(a.TimeTable.Day), a.Student.StudentName })
                //    .Select(a => new StudentAtt
                //    {
                //        Date = a.Key.Date,
                //        Student_name = a.Key.StudentName
                //    }).OrderBy(a => a.Date).ToList();

            }

            return View(empWithDate);
        }

        // GET: Attendances/Details/5
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

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            ViewBag.TimeID = new SelectList(db.TimeTables, "TimeID", "ClassID");
            ViewBag.AttendanceTypeID = new SelectList(db.AttendanceTypes, "AttendanceTypeID", "AttendanceTypeName");
            return View();
        }

        // POST: Attendances/Create
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

        // GET: Attendances/Edit/5
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

        // POST: Attendances/Edit/5
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

        // GET: Attendances/Delete/5
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

        // POST: Attendances/Delete/5
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
