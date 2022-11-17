using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZealEducationMgt.Models;

namespace ZealEducationMgt.Controllers
{
    public class TeacherSubjectController : Controller
    {
        private ZealEducationMgtDbEntities dbContext = new ZealEducationMgtDbEntities();
        // GET: TeacherSubject
        public ActionResult Index(string name)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var topMovie = (from sn in dbContext.MultiSubjectTeachers
                            join mv in dbContext.Teachers on sn.TeacherID equals mv.TeacherID
                            group sn by new
                            {
                                sn.TeacherID

                            } into c
                            select new teachersubject
                            {
                                teacherid = c.FirstOrDefault().TeacherID,


                                TeacherName = c.FirstOrDefault().Teacher.TeacherName


                            }).ToList();
            if (!String.IsNullOrEmpty(name))
            {
                topMovie = (from sn in dbContext.MultiSubjectTeachers

                            join mv in dbContext.Teachers on sn.TeacherID equals mv.TeacherID
                            where sn.Teacher.TeacherName.Contains(name) || sn.Subject.SubjectName.Contains(name)
                            group sn by new
                            {
                                sn.TeacherID
                            } into c
                            select new teachersubject
                            {
                                teacherid = c.FirstOrDefault().TeacherID,

                                TeacherName = c.FirstOrDefault().Teacher.TeacherName


                            }).ToList();
            }
            if (name == "")
            {
                return RedirectToAction("Index");
            }
            ViewBag.name = name;
            ViewBag.TopMovie = topMovie;
            ViewBag.MovieTypes = dbContext.MultiSubjectTeachers;
            ViewBag.Subject = dbContext.Subjects.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.FacultyTables = dbContext.Faculties.ToList();


            return View();
        }

        public ActionResult FillCity(string id)
        {
           
            var cities = (from p in dbContext.Teachers
                                    where p.FacultyID.Equals(id)
                                    select new Teacherab
                                    {
                                        teacherID = p.TeacherID,
                                        teacherName = p.TeacherName

                                    }).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Subject(string id)
        {
            var data = (from p in dbContext.Subjects
                        where p.FacultyID.Equals(id)
                        select new Subjectg
                        {
                            subjectID = p.SubjectID,
                            subjectName = p.SubjectName

                        }).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]

        public JsonResult Create(string teacherID, string[] subjectIDs)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return Json(new { result = false, newurl = Url.Action("Index", "TeacherSubject") }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                foreach (string subjectid in subjectIDs)
                {
                    MultiSubjectTeacher t = new MultiSubjectTeacher();
                    t.TeacherID = teacherID;
                    t.SubjectID = subjectid;
                    dbContext.MultiSubjectTeachers.Add(t);
                    dbContext.SaveChanges();
                }
                return Json(new { result = true, newurl = Url.Action("Index", "TeacherSubject") }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {
                return Json(new { Result = false }, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult GetById(string id)
        {

            dbContext.Configuration.ProxyCreationEnabled = false;
            var item = (from p in dbContext.MultiSubjectTeachers
                        where p.TeacherID.Equals(id)
                        select new Teacherab
                        {
                            teacherID = p.TeacherID,
                            facultyID = p.Teacher.FacultyID

                        }).FirstOrDefault();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById1(string id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var item = (from p in dbContext.Subjects
                        where p.FacultyID.Equals(id)
                        select new Subjectg
                        {
                            subjectID = p.SubjectID,
                            subjectName = p.SubjectName

                        }).ToList();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById2(string id, string subject)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var item = dbContext.MultiSubjectTeachers.Where(c => c.TeacherID.Equals(id)).ToList();
            var pro = item.Where(x => x.SubjectID.Equals(subject)).Count();
            return Json(pro, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Edit(string teacherID, string[] subjectIDs)
        {

            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return Json(new { result = false, newurl = Url.Action("Index", "TeacherSubject") }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                DeleteType(teacherID);
                foreach (string subjectid in subjectIDs)
                {

                    MultiSubjectTeacher t = new MultiSubjectTeacher();



                    t.TeacherID = teacherID;
                    t.SubjectID = subjectid;
                    dbContext.MultiSubjectTeachers.Add(t);

                    dbContext.SaveChanges();

                }


                return Json(new { success = true });



            }
            catch (Exception)
            {

                return Json(new { success = false });
            }


        }
        public void DeleteType(string id)
        {
            var type = dbContext.MultiSubjectTeachers.Where(x => x.TeacherID.Equals(id)).ToList();
            foreach (var item in type)
            {
                dbContext.MultiSubjectTeachers.Remove(item);
            }
        }
        [HttpPost]
        public JsonResult DeleteMultiple(string id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return Json(new { result = false, newurl = Url.Action("Index", "TeacherSubject") }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                DeleteType(id);
                dbContext.SaveChanges();
                return Json(new { success = true });
            }
            catch
            {
                return Json(new { success = false });
            }

        }
        [HttpPost]
        public JsonResult CheckTime(string _teacher)
        {

            var data = dbContext.MultiSubjectTeachers.Where(x => x.TeacherID.Equals(_teacher)).Count();
            if (data > 0)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}