using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCempty.Models;

namespace MVCempty.Controllers
{
    public class SuppliersController : Controller
    {
        datalinqDataContext db = new datalinqDataContext();

        // GET: Currencies
        public ActionResult Index()
        {
            List<supplier> model = new List<supplier>();
            model = db.suppliers.ToList();
            return View(model);
        }

        public ActionResult AddSupplier()
        {
            if (Request.Form["name"] != null)
            {
                supplier sup = new supplier
                {
                    name = Request.Form["name"].ToString(),
                };

                db.suppliers.InsertOnSubmit(sup);

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
                return RedirectToAction("Index", "Suppliers");
            }
            return View();
        }


        public ActionResult EditSupplier(int id)
        {
            if (Request.Form["name"] != null)
            {
                var cur = (from supplier in db.suppliers
                           where supplier.supplier_id == id
                           select supplier).SingleOrDefault();

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
                return RedirectToAction("Index", "Suppliers");
            }
            else
            {
                var cur = (from supplier in db.suppliers
                           where supplier.supplier_id == id
                           select supplier).SingleOrDefault();
                ViewBag.Supplier = cur;
                return View();
            }
        }


        public ActionResult DeleteSupplier(int id)
        {
            var cur = (from supplier in db.suppliers
                       where supplier.supplier_id == id
                       select supplier).SingleOrDefault();
            db.suppliers.DeleteOnSubmit(cur);


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

            return RedirectToAction("Index", "Suppliers");
        }
    }
}