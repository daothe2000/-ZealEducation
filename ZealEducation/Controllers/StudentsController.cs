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
    public class StudentsController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        // GET: Students
        public ActionResult Index(string search_id, string search_name)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var student = db.Students.ToList();
            if (!String.IsNullOrEmpty(search_id))
            {
                student = student.Where(s => s.StudentID.Contains(search_id)).ToList();
            }
            if (!String.IsNullOrEmpty(search_name))
            {
                student = student.Where(s => s.StudentName.Contains(search_name)).ToList();
            }
            return View(student);
        }

        // GET: Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student studentTable = db.Students.Find(id);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            return View(studentTable);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,StudentName,Phone,Address,Gender,Password,Email,Image,Birthday,status,Educationlevel")] Student studentTable, HttpPostedFileBase image)
        {
            var findId = db.Students.Where(i => i.StudentID.Equals(studentTable.StudentID)).Count();
            var findPass = db.Students.Where(i => i.Password.Equals(studentTable.Password)).Count();
            try
            {
                if (ModelState.IsValid)
                {
                    if (findId == 0  && findPass == 0)
                    {
                        if (image.FileName != null)
                        {
                            image.SaveAs(Server.MapPath("~/Content/AdminTempale/img/figure/" + image.FileName));
                            studentTable.Image = "" + image.FileName;
                        }
                        db.Students.Add(studentTable);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (findId != 0 && findPass == 0)
                    {
                        ViewBag.errorId = "<div class='alert alert-danger'>Student Id existed</div> ";
                    }
                    else if (findId == 0 &&  findPass != 0)
                    {
                        ViewBag.errorPass = "<div class='alert alert-danger'>Password existed</div> ";
                    }
                    else
                    {
                        ViewBag.errorId = "<div class='alert alert-danger'>Student Id existed</div> ";
                        ViewBag.errorPass = "<div class='alert alert-danger'>Password existed</div> ";
                    }
                }
            }
            catch (Exception)
            {

                return View(studentTable);
            }
            return View(studentTable);
        }

        public List<SelectListItem> getStatus()
        {
            var sttus = new[] {
                new SelectListItem{ Value = "0", Text = "Please Select Status *" },
                new SelectListItem{ Value = "1", Text = "Đang hoạt động" },
                new SelectListItem{ Value = "2", Text = "Vô hiệu hóa"},
            };
            return sttus.ToList();
        }
        public List<SelectListItem> getStt()
        {
            var stt = new[] {
                new SelectListItem{ Value = "0", Text = "Please Select Gender *" },
                new SelectListItem{ Value = "1", Text = "Male" },
                new SelectListItem{ Value = "2", Text = "Female"},
            };
            return stt.ToList();
        }

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student studentTable = db.Students.Find(id);
            var chk = 0;
            var chk_status = 0;
            if (studentTable.Status == true)
            {
                chk_status = 1;
            }
            else
            {
                chk_status = 2;
            }
            ViewBag.getSttus = new SelectList(getStatus(), "Value", "Text", chk_status);

            if (studentTable.Gender)
            {
                chk = 1;
            }
            else
            {
                chk = 2;
            }
            ViewBag.getGen = new SelectList(getStt(), "Value", "Text", chk);
            if (studentTable == null)
            {
                return HttpNotFound();
            }
            return View(studentTable);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,StudentName,Phone,Address,Gender,Password,Email,Image,Birthday,status,Educationlevel")] Student studentTable, int getGen, int getSttus, HttpPostedFileBase image, string img)
        {

            if (ModelState.IsValid)
            {
                if (getSttus == 1)
                {
                    studentTable.Status = true;
                }
                else if (getSttus == 2)
                {
                    studentTable.Status = false;
                }
                if (getGen == 1)
                {
                    studentTable.Gender = true;
                }
                else if (getGen == 2)
                {
                    studentTable.Gender = false;
                }
                
                db.Entry(studentTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var chk = 0;
            if (studentTable.Gender)
            {
                chk = 1;
            }
            else
            {
                chk = 2;
            }
            ViewBag.getGen = new SelectList(getStt(), "Value", "Text", chk);
            var chk_status = 0;
            if (studentTable.Status==true)
            {
                chk_status = 1;
            }
            else
            {
                chk_status = 2;
            }
            ViewBag.getSttus = new SelectList(getStatus(), "Value", "Text", chk_status);
            return View(studentTable);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
          
            try
            {
                Student studentTable = db.Students.Single(x => x.StudentID.Equals(id));
                db.Students.Remove(studentTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["error"] = "DELETE FAILED";
                return RedirectToAction("Index");
            }
        }

        // POST: Students/Delete/5


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
