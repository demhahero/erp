using MVCempty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCempty.Controllers
{
    public class CustomersController : Controller
    {
        datalinqDataContext db = new datalinqDataContext();

        // GET: Currencies
        public ActionResult Index()
        {
            List<customer> model = new List<customer>();
            model = db.customers.ToList();
            return View(model);
        }

        public ActionResult AddCustomer()
        {
            if (Request.Form["name"] != null)
            {
                customer pro = new customer
                {
                    name = Request.Form["name"].ToString(),
                };

                db.customers.InsertOnSubmit(pro);

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // Make some adjustments. 
                    // ... 
                    // Try again.
                    db.SubmitChanges();
                }
                return RedirectToAction("Index", "Customers");
            }
            return View();
        }


        public ActionResult EditCustomer(int id)
        {
            if (Request.Form["name"] != null)
            {
                var cur = (from customer in db.customers
                           where customer.customer_id == id
                           select customer).SingleOrDefault();

                cur.name = Request.Form["name"].ToString();

                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    // Make some adjustments. 
                    // ... 
                    // Try again.
                    db.SubmitChanges();
                }
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                var cur = (from customer in db.customers
                           where customer.customer_id == id
                           select customer).SingleOrDefault();
                ViewBag.Customer = cur;
                return View();
            }
        }


        public ActionResult DeleteCustomer(int id)
        {
            var cur = (from customer in db.customers
                       where customer.customer_id == id
                       select customer).SingleOrDefault();
            db.customers.DeleteOnSubmit(cur);


            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Make some adjustments. 
                // ... 
                // Try again.
                db.SubmitChanges();
            }

            return RedirectToAction("Index", "Customers");
        }
    }
}