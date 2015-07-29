using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCempty.Models;

namespace MVCempty.Controllers
{
    public class CurrenciesController : Controller
    {
        datalinqDataContext db = new datalinqDataContext();

        // GET: Currencies
        public ActionResult Index()
        {
            List<currency> model = new List<currency>();
            model = db.currencies.ToList();
            return View(model);
        }

        public ActionResult AddCurrency()
        {
            if (Request.Form["name"] != null)
            {
                currency pro = new currency
                {
                    name = Request.Form["name"].ToString(),
                };

                db.currencies.InsertOnSubmit(pro);

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
                return RedirectToAction("Index", "Currencies");
            }
            return View();
        }


        public ActionResult EditCurrency(int id)
        {
            if (Request.Form["name"] != null)
            {
                var cur = (from currency in db.currencies
                           where currency.currency_id == id
                           select currency).SingleOrDefault();

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
                return RedirectToAction("Index", "Currencies");
            }
            else
            {
                var cur = (from currency in db.currencies
                           where currency.currency_id == id
                           select currency).SingleOrDefault();
                ViewBag.Currency = cur;
                return View();
            }
        }


        public ActionResult DeleteCurrency(int id)
        {
            var cur = (from currency in db.currencies
                       where currency.currency_id == id
                       select currency).SingleOrDefault();
            db.currencies.DeleteOnSubmit(cur);


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

            return RedirectToAction("Index", "Currencies");
        }
    }
}