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

        // GET: Currencies
        public ActionResult ExchangerIndex()
        {
            List<exchanger> model = new List<exchanger>();
            model = db.exchangers.ToList();
            List<ExchangerModel> em = new List<ExchangerModel>();
            foreach (var x in model)
            {
                ExchangerModel exm = new ExchangerModel();
                var cur1 = (from currency in db.currencies
                           where currency.currency_id == x.currency_id1
                           select currency).SingleOrDefault();
                exm.currency1 = cur1;

                var cur2 = (from currency in db.currencies
                            where currency.currency_id == x.currency_id2
                            select currency).SingleOrDefault();
                exm.currency2 = cur2;

                exm.value = x.value;
                em.Add(exm);
            }
            return View(em);
        }

        public ActionResult AddExchanger()
        {
            if (Request.Form["currency_id1"] != null && Request.Form["currency_id2"] != null && Request.Form["value"] != null)
            {
                exchanger exch = new exchanger
                {
                    currency_id1 = Convert.ToInt32(Request.Form["currency_id1"].ToString()),
                    currency_id2 = Convert.ToInt32(Request.Form["currency_id2"].ToString()),
                    value = Convert.ToDouble(Request.Form["value"].ToString())
                };

                db.exchangers.InsertOnSubmit(exch);

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
                return RedirectToAction("ExchangerIndex", "Currencies");
            }


            List<currency> Currencies = new List<currency>();
            Currencies = db.currencies.ToList();
            ViewData["Currencies"] = Currencies;

            return View();
        }

        public ActionResult EditExchanger(int id , int id2)
        {
            if (Request.Form["value"] != null)
            {
                var exch = (from exchanger in db.exchangers
                           where exchanger.currency_id1 == id && exchanger.currency_id2 == id2
                           select exchanger).SingleOrDefault();

                exch.value = Convert.ToDouble(Request.Form["value"].ToString());

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
                return RedirectToAction("ExchangerIndex", "Currencies");
            }
            else
            {
                var exch = (from exchanger in db.exchangers
                           where exchanger.currency_id1 == id && exchanger.currency_id2 == id2
                            select exchanger).SingleOrDefault();
                var cur1 = (from currency in db.currencies
                            where currency.currency_id == exch.currency_id1
                            select currency).SingleOrDefault();
                var cur2 = (from currency in db.currencies
                            where currency.currency_id == exch.currency_id2
                            select currency).SingleOrDefault();

                ExchangerModel exchm = new ExchangerModel();
                exchm.currency1 = cur1;
                exchm.currency2 = cur2;
                exchm.value = exch.value;

                ViewData["ExchangerModel"] = exchm;

                List<currency> Currencies = new List<currency>();
                Currencies = db.currencies.ToList();
                ViewData["Currencies"] = Currencies;
                ViewData["ExchangerModel"] = exchm;
                return View();
            }
        }

        public ActionResult DeleteExchanger(int id, int id2)
        {
            var exch = (from exchanger in db.exchangers
                        where exchanger.currency_id1 == id && exchanger.currency_id2 == id2
                        select exchanger).SingleOrDefault();
            db.exchangers.DeleteOnSubmit(exch);

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

            return RedirectToAction("ExchangerIndex", "Currencies");
        }

    }
}