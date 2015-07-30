using MVCempty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCempty.Controllers
{
    public class StoresController : Controller
    {
        datalinqDataContext db = new datalinqDataContext();

        // GET: Stores
        public ActionResult Index()
        {
            List<store> model = new List<store>();
            model = db.stores.ToList();
            return View(model);
        }



        public ActionResult AddStore()
        {
            if (Request.Form["name"] != null)
            {
                store store = new store
                {
                    name = Request.Form["name"].ToString(),
                };

                db.stores.InsertOnSubmit(store);

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
                return RedirectToAction("Index", "Stores");
            }
            return View();
        }


        public ActionResult EditStore(int id)
        {
            if (Request.Form["name"] != null)
            {
                var sto = (from store in db.stores
                           where store.store_id == id
                           select store).SingleOrDefault();

                sto.name = Request.Form["name"].ToString();

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
                return RedirectToAction("Index", "Stores");
            }
            else
            {
                var sto = (from store in db.stores
                           where store.store_id == id
                           select store).SingleOrDefault();
                ViewBag.Store = sto;
                return View();
            }
        }


        public ActionResult DeleteStore(int id)
        {
            var sto = (from store in db.stores
                       where store.store_id == id
                       select store).SingleOrDefault();
            db.stores.DeleteOnSubmit(sto);


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

            return RedirectToAction("Index", "Stores");
        }
    }
}