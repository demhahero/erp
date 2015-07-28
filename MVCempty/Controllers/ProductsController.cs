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
            var pro = from product in db.products where product.product_id == id
                                        select product;
            foreach (var pros in pro)
            {
                db.products.DeleteOnSubmit(pros);
            }

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
                    currency_id = 1
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
            return View();
        }
    }
}