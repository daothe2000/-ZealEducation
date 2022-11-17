using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZealEducationMgt.Controllers
{
    public class ParentTableController : Controller
    {
        // GET: ParentTable
        private ZealEducationMgtDbEntities dbContext = new ZealEducationMgtDbEntities();
        // GET: Admin/Shop_Product


        public ActionResult Index(string name)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var ParentTable = dbContext.Parents.ToList();
            
            if (!String.IsNullOrEmpty(name))
            {
                ParentTable = dbContext.Parents.Where(s => s.ParentName.Contains(name) || s.Address.Contains(name)).ToList();
            }

            if (name == "")
            {
                return RedirectToAction("Index");
            }
            ViewBag.name = name;
            return View(ParentTable);
        }


        public ActionResult Create()
        {
           
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Parent pa)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var parent = dbContext.Parents.Where(x => x.Phone.Equals(pa.Phone)).Count();
            var parent1 = dbContext.Parents.Where(x => x.Email.Equals(pa.Email)).Count();
            try
            {
                
                if (ModelState.IsValid)
                {
                    if (parent == 0 && parent1 == 0)
                    {

                        pa.Password = new AccountModel().MD5Hash(pa.Password);
                      
                        dbContext.Parents.Add(pa);
                        dbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (parent != 0 && parent1 == 0)
                    {
                        ModelState.AddModelError("Phone", "Phone already exists!");

                    }
                    else if (parent1 != 0 && parent == 0)
                    {
                        ModelState.AddModelError("Email", "Email already exists!");
                    }
                    else
                    {
                        ModelState.AddModelError("Phone", "Phone already exists!");
                        ModelState.AddModelError("Email", "Email already exists!");
                    }

                }

            }
            catch
            {
                return View();
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var result = dbContext.Parents.Single(x => x.ParentID == id);
          

            return View(result);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Parent t)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var parenttable = dbContext.Parents.Where(x => x.Phone.Equals(t.Phone)).Count();
            var parenttable1 = dbContext.Parents.Where(x => x.Email.Equals(t.Email)).Count();
            var parenttable11 = dbContext.Parents.Find(id);

            try
            {
                
                if (ModelState.IsValid)
                {
                    Parent parent = dbContext.Parents.Single(x => x.ParentID == id);
                    parent.ParentName = t.ParentName;
                    parent.Address = t.Address;
                    parent.Gender = t.Gender;
                    parent.Password = new AccountModel().MD5Hash(t.Password);



                    if (parenttable11.Phone == t.Phone && parenttable11.Email == t.Email)
                    {

                        dbContext.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else if (parenttable11.Phone == t.Phone && parenttable11.Email != t.Email)
                    {
                        if (parenttable1 == 0)
                        {

                            parent.Email = t.Email;
                            dbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("Email", "Email already exists!");

                        }
                    }
                    else if (parenttable11.Phone != t.Phone && parenttable11.Email == t.Email)
                    {
                        if (parenttable == 0)
                        {

                            parent.Phone = t.Phone;
                            dbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("Phone", "Phone already exists!");

                        }
                    }
                    else
                    {
                        if (parenttable == 0 && parenttable1 == 0)
                        {

                            parent.Phone = t.Phone;

                            parent.Email = t.Email;


                            dbContext.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else if (parenttable != 0 && parenttable1 == 0)
                        {
                            ModelState.AddModelError("Phone", "Phone already exists!");

                        }
                        else if (parenttable1 != 0 && parenttable == 0)
                        {
                            ModelState.AddModelError("Email", "Email already exists!");
                        }
                        else
                        {
                            ModelState.AddModelError("Phone", "Phone already exists!");
                            ModelState.AddModelError("Email", "Email already exists!");
                        }
                    }
                }

            }
            catch
            {
                return View(t);
            }
            return View(t);
        }


        public ActionResult Delete(int id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var result = dbContext.Parents.Single(x => x.ParentID == id);

            
            return View(result);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Parent t)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var result = dbContext.Parents.Single(x => x.ParentID == id);
                dbContext.Parents.Remove(result);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.error = "<div class='alert alert-danger'> delete failed</div>";
                return View(t);
            }
           
        }
    }
}