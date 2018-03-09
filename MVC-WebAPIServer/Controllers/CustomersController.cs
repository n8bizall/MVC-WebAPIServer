using MVC_WebAPIServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;




namespace MVC_WebAPIServer.Models
{
    public class CustomersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return Json(db.Customers.ToList(), JsonRequestBehavior.AllowGet);

        }

        //should be called with Customer/Get/5
        public ActionResult Get(int? id)
        {
            if (id == null)
            {
                return Json(new JsonMessage("Failure", "Id is null"), JsonRequestBehavior.AllowGet);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return Json(new JsonMessage("Failure", "Id is not found"), JsonRequestBehavior.AllowGet);
            }
            return Json(customer, JsonRequestBehavior.AllowGet);

        } //Customers/Create  [POST]
        public ActionResult Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return Json(new JsonMessage("Failure", "ModelState is not valid"), JsonRequestBehavior.AllowGet);
            }
            db.Customers.Add(customer);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonMessage("Success", "Customer was created the new id is:"), JsonRequestBehavior.AllowGet); //add employee id return data





        }





        public ActionResult Remove([FromBody] Customer  customer)
        {
            Customer customer2 = db.Customers.Find(customer.Id);
            db.Customers.Remove(customer2);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Customer was deleted successfully"), JsonRequestBehavior.AllowGet);
        }
        //Employees/Change
        public ActionResult Change([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return Json(new JsonMessage("Failure", "The record has already been deleted,not found"), JsonRequestBehavior.AllowGet);
            }
            Customer customer2 = db.Customers.Find(customer.Id);
            customer2.Id = customer.Id;
            customer2.Name = customer.Name;
            customer2.CreditLimit = customer.CreditLimit;
            customer2.Active = customer.Active;
            try
            {
                // db.SaveChanges();
            }
            catch (Exception ex)
            {
                return Json(new JsonMessage("Failure", ex.Message), JsonRequestBehavior.AllowGet);
            }
            return Json(new JsonMessage("Success", "Department was changed"), JsonRequestBehavior.AllowGet);
        }
    }
}