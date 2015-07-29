using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCempty.Models;
namespace MVCempty.Controllers
{
    public class OrdersController : Controller
    {
        datalinqDataContext db = new datalinqDataContext();

        // GET: Orders
        public ActionResult Index()
        {
            var ords = from order in db.orders select order;
            return View(ords);
        }

        public ActionResult AddOrder()
        {
            if (Request.Form["product_id"] != null)
            {
                order ord = new order
                {
                    product_id = Convert.ToInt32(Request.Form["product_id"].ToString()),
                    customer_id = Convert.ToInt32(Request.Form["customer_id"].ToString()),
                    quantity = Convert.ToInt32(Request.Form["quantity"].ToString()),
                    currency_id = Convert.ToInt32(Request.Form["currency_id"].ToString())
            };
                db.orders.InsertOnSubmit(ord);

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
                return RedirectToAction("Index", "Orders");
            }

            List<customer> Customers = new List<customer>();
            Customers = db.customers.ToList();
            ViewData["Customers"] = Customers;

            List<currency> Currencies = new List<currency>();
            Currencies = db.currencies.ToList();
            ViewData["Currencies"] = Currencies;

            List<product> model = new List<product>();
            model = db.products.ToList();
            return View(model);

        }

        public ActionResult EditOrder(int id)
        {
            if (Request.Form["product_id"] != null)
            {
                var ord = (from order in db.orders
                           where order.order_id == id
                           select order).SingleOrDefault();

                ord.product_id = Convert.ToInt32(Request.Form["product_id"].ToString());
                ord.customer_id = Convert.ToInt32(Request.Form["customer_id"].ToString());
                ord.currency_id = Convert.ToInt32(Request.Form["currency_id"].ToString());
                ord.quantity = Convert.ToInt32(Request.Form["quantity"].ToString());

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
                return RedirectToAction("Index", "Orders");
            }
            else
            {
                var ord = (from order in db.orders
                           where order.order_id == id
                           select order).SingleOrDefault();
                ViewBag.Order = ord;

                List<customer> Customers = new List<customer>();
                Customers = db.customers.ToList();
                ViewData["Customers"] = Customers;

                List<currency> Currencies = new List<currency>();
                Currencies = db.currencies.ToList();
                ViewData["Currencies"] = Currencies;

                List<product> model = new List<product>();
                model = db.products.ToList();
                return View(model);
            }
        }

        public ActionResult DeleteOrder(int id)
        {
            var ord = (from order in db.orders
                       where order.order_id == id
                       select order).SingleOrDefault();
            db.orders.DeleteOnSubmit(ord);
   

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

            return RedirectToAction("Index", "Orders");
        }
    }
}