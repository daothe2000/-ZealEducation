using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using DataAccess;
using ZealEducationMgt.Models;

namespace ZealEducationMgt.Controllers
{
    public class RoomsController : Controller
    {
        private ZealEducationMgtDbEntities db = new ZealEducationMgtDbEntities();

        // GET: Students
        public ActionResult Index(string search_name)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            search_name = search_name ?? "";
            var sub = db.Rooms.Where(s => s.RoomName.Contains(search_name));
            ViewBag.name = search_name;
            return View(sub.ToList());
        }


        


        [HttpPost]
        public ActionResult Create(Room model)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            
            if (model.RoomID > 0)
            {
                var item = db.Rooms.Find(model.RoomID);
                item.RoomName = model.RoomName;
                item.Status = model.Status;

                db.Entry(item).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                    

                    return Json(new { success = true, edit = true });
                }
                catch (Exception)
                {
                  
                    return Json(new { success = false, edit = true });
                }
            }
            else
            {
                db.Rooms.Add(model);
                try
                {
                    db.SaveChanges();
                   
                    return Json(new { success = true, edit = false });
                }
                catch (Exception)
                {
                   
                    return Json(new { success = false, edit = false });
                }
            }

        }
        
        public JsonResult GetById(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var item = db.Rooms.Where(c => c.RoomID == id).FirstOrDefault();
            return Json(item, JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult UpdateStatus(int id, bool status)
        {


            var item = db.Rooms.Find(id);

            if (item != null)
            {

                item.Status = status;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new { item = false }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (Session["userType"] == null || !Session["userType"].Equals("Staff") && string.IsNullOrEmpty(Convert.ToString(Session["StaffID"])))
            {
                return RedirectToAction("Index", "Login");
            }
            try
            {
                var cate = db.Rooms.Where(x => x.RoomID == id).FirstOrDefault();
                db.Rooms.Remove(cate);

                db.SaveChanges();
                return Json(new { status = true, success = true });
            }
            catch
            {
                return Json(new { status = false, success = false });
            }
        }
    }
}
