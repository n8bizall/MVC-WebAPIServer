using MVC_WebAPIServer.Models;
using MVC_WebAPIServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MVC_WebAPIServer.Controllers
{
    public class OrdersController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchCriteria)
        {
            if (searchCriteria == null)
            {
                return Json(new JsonMessage("Failure", "SearchCriteria is null"), JsonRequestBehavior.AllowGet);
            }
            List<Order> orders = db.Orders.Where(d => d.Description.Contains(searchCriteria)).ToList();
            return Json(orders, JsonRequestBehavior.AllowGet);
        }




        public ActionResult List()
        {
            return Json(db.Orders.ToList(), JsonRequestBehavior.AllowGet);

        }

        //should be called with Order/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(order, JsonRequestBehavior.AllowGet);

        } //Order/Create  [POST]
        public ActionResult Create([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Orders.Add(order);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonMessage("Success", "Order was created the new id is:"), JsonRequestBehavior.AllowGet); //add employee id return data





        }





        public ActionResult Remove([FromBody] Order order)
        {
            Order order2 = db.Orders.Find(order.Id);
            db.Orders.Remove(order2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Order was deleted successfully"), JsonRequestBehavior.AllowGet);
        }
        //Orders/Change
        public ActionResult Change([FromBody] Order order)
        {
            if (order == null)
            {
                return Json(new JsonMessage("Failure", "The record has already been deleted,not found"), JsonRequestBehavior.AllowGet);
            }
            Order order2 = db.Orders.Find(order.Id);
            order2.Id = order.Id;
            order2.CustomerId = order.CustomerId;
            order2.Total = order.Total;
            order2.Fulfilled = order.Fulfilled;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Order was changed"), JsonRequestBehavior.AllowGet);
        }
    }
}