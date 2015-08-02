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

                int i = 1;
                while (Request.Form["item" + i] != null)
                {

                    var s_p = (from store_product in db.store_products
                               where store_product.store_id == id && store_product.product_id == Convert.ToInt32(Request.Form["item" + i].ToString())
                               select store_product).SingleOrDefault();
                    if (s_p == null)
                    {
                        store_product sto_pro = new store_product
                        {
                            product_id = Convert.ToInt32(Request.Form["item" + i].ToString()),
                            quantity = Convert.ToInt32(Request.Form["itemvalue" + i].ToString()),
                            store_id = id
                        };

                        db.store_products.InsertOnSubmit(sto_pro);


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
                    }
                    else
                    {
                        s_p.quantity = Convert.ToInt32(Request.Form["itemvalue" + i].ToString());
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
                    }
                    i++;
                }

                return RedirectToAction("Index", "Stores");
            }
            else
            {

                var pro = from store_product in db.store_products
                          where store_product.store_id == id
                          select store_product;

                ViewData["store_products"] = pro;

                var sto = (from store in db.stores
                           where store.store_id == id
                           select store).SingleOrDefault();
                ViewBag.Store = sto;
                List<product> model = new List<product>();
                model = db.products.ToList();
                return View(model);
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