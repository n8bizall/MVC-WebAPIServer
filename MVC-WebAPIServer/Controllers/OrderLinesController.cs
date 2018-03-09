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
    public class OrderLinesController : Controller
    {

        private AppDbContext db = new AppDbContext();
        // GET: OrderLines
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult List()
        {
            CalcLineTotal();
            return Json(db.OrderLines.ToList(), JsonRequestBehavior.AllowGet);

        }

        //should be called with OrderLines/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            OrderLine orderline = db.OrderLines.Find(id);
            if (orderline == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(orderline, JsonRequestBehavior.AllowGet);

        }

        //OrderLines/Create  [POST]
        public ActionResult Create([FromBody] OrderLine orderline)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.OrderLines.Add(orderline);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonMessage("Success", "OrderLine was created the new id is:"), JsonRequestBehavior.AllowGet); //add employee id return data
        }

        public ActionResult Remove([FromBody] OrderLine orderline)
        {
            OrderLine orderline2 = db.OrderLines.Find(orderline.Id);
            db.OrderLines.Remove(orderline2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "OrderLine was deleted successfully"), JsonRequestBehavior.AllowGet);
        }

        //OrderLine/Change
        public ActionResult Change([FromBody] OrderLine orderline)
        {
            if (orderline == null)
            {
                return Json(new JsonMessage("Failure", "The record has already been deleted,not found"), JsonRequestBehavior.AllowGet);
            }
            OrderLine orderline2 = db.OrderLines.Find(orderline.Id);
            orderline2.Id = orderline.Id;
            orderline2.OrderId = orderline.OrderId;
            orderline2.LineNbr = orderline.LineNbr;
            orderline2.Product = orderline.Product;
            orderline2.Price = orderline.Price;
            orderline2.Quantity = orderline.Quantity;
            orderline2.LineTotal = orderline.LineTotal;
            try
            {
                 db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "OrderLine was changed"), JsonRequestBehavior.AllowGet);
        }


    }
}