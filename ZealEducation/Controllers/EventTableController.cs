using DataAccess;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZealEducationMgt.Controllers
{
    public class EventTableController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();
        // GET: EventTable
        public ActionResult Index()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var result = db.Events.ToList();
            return View(result);
        }

   
        public ActionResult Create()
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event e)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (e.startDate <= e.endDate)
                    {
                        e.status = true;
                       db.Events.Add(e);

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("StartDate", "Start must be lesser than EndTime");
                        ModelState.AddModelError("EndDate", "EndDate must be greater than StartTime");
                    }

                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult UpdateStatus(int id, bool status)
        {


            var result = db.Events.Find(id);

            if (result != null)
            {

                result.status = status;
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(result, JsonRequestBehavior.AllowGet);






        }
        public ActionResult Delete(int? id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            var del = db.Events.Find(id);
            if (del != null)
            {
                db.Events.Remove(del);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //[Authorize(Roles = "Admin,User")]
        //public ActionResult Edit(int id)
        //{
        //    var result = dbContext.Events.Single(x => x.Event_id == id);
        //    ViewBag.Shop_Id = new SelectList(dbContext.Shops.ToList(), "ShoId", "ShopName", result.Shop_ShoId);

        //    return View(result);

        //}

        //[Authorize(Roles = "Admin,User")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, Event e)
        //{
        //    try
        //    {
        //        ViewBag.Shop_Id = new SelectList(dbContext.Shops.ToList(), "ShoId", "ShopName", e.Shop_ShoId);
        //        if (ModelState.IsValid)
        //        {
        //            if (e.StartDate < e.EndDate)
        //            {
        //                Event events = dbContext.Events.Single(x => x.Event_id == id);
        //                events.Shop_ShoId = e.Shop_ShoId;
        //                events.StartDate = e.StartDate;
        //                events.Price = e.Price;
        //                events.Descriptions = e.Descriptions;
        //                events.EndDate = e.EndDate;
        //                events.Image = e.Image;
        //                events.Address = e.Address;
        //                events.Title = e.Title;
        //                events.slug = e.slug;
        //                events.Meta_title = e.Meta_title;
        //                events.Meta_keyword = e.Meta_keyword;
        //                events.Meta_description = e.Meta_description;
        //                events.ModifiedDate = DateTime.Now;
        //                events.Status = e.Status;
        //                dbContext.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("StartDate", "Start must be lesser than EndTime");
        //                ModelState.AddModelError("EndDate", "EndDate must be greater than StartTime");
        //            }
        //        }

        //    }
        //    catch
        //    {
        //        return View(e);
        //    }

        //    return View(e);
        //}

        //[Authorize(Roles = "Admin,User")]
        //public ActionResult Delete(int id)
        //{
        //    var result = dbContext.Events.Single(x => x.Event_id == id);
        //    ViewBag.Shop_Id = new SelectList(dbContext.Shops.ToList(), "ShoId", "ShopName", result.Shop_ShoId);
        //    return View(result);
        //}

        //[Authorize(Roles = "Admin,User")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, Shop s)
        //{
        //    try
        //    {
        //        var result = dbContext.Events.Single(x => x.Event_id == id);
        //        dbContext.Events.Remove(result);
        //        dbContext.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View(s);
        //    }
        //}
    }
}