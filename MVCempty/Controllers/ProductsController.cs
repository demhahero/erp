using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCempty.Models;

namespace MVCempty.Controllers
{
    public class ProductsController : Controller
    {
        datalinqDataContext db = new datalinqDataContext();

        // GET: Products
        public ActionResult Index()
        {
            List<product> model = new List<product>();
            model = db.products.ToList();
            return View(model);
        }

        public ActionResult DeleteProduct(int id)
        {
            var pro = (from product in db.products where product.product_id == id
                      select product).SingleOrDefault();
   
            db.products.DeleteOnSubmit(pro);

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

            return RedirectToAction("Index", "Products");
        }

        public ActionResult AddProduct()
        {
            if (Request.Form["name"] != null)
            {
                product pro = new product
                {
                    name = Request.Form["name"].ToString(),
                    price = Convert.ToDouble(Request.Form["price"].ToString()),
                    currency_id = Convert.ToInt32(Request.Form["currency_id"].ToString())
                };
                db.products.InsertOnSubmit(pro);
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
                return RedirectToAction("Index", "Products");
            }

            List<currency> Currencies = new List<currency>();
            Currencies = db.currencies.ToList();
            ViewData["Currencies"] = Currencies;
            return View();
        }

        public ActionResult EditProduct(int id)
        {
            if (Request.Form["name"] != null)
            {
                var pro = (from product in db.products
                           where product.product_id == id
                           select product).SingleOrDefault();

                pro.name = Request.Form["name"].ToString();
                pro.price = Convert.ToDouble(Request.Form["price"].ToString());
                pro.currency_id = Convert.ToInt32(Request.Form["currency_id"].ToString());

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
                return RedirectToAction("Index", "Products");
            }
            else
            {
                var pro = (from product in db.products
                           where product.product_id == id
                           select product).SingleOrDefault();
                ViewBag.Product = pro;

                List<currency> Currencies = new List<currency>();
                Currencies = db.currencies.ToList();
                ViewData["Currencies"] = Currencies;

                return View();
            }
        } 
    }
}