using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCempty.Models;
namespace MVCempty.Controllers
{
    public class AccountsController : Controller
    {
        datalinqDataContext db = new datalinqDataContext();
        // GET: Accounts
        public ActionResult Index()
        {
            List<account> model = new List<account>();
            model = db.accounts.ToList();
            return View(model);
        }

        public ActionResult AddAccount()
        {
            if (Request.Form["number"] != null)
            {
                account acc = new account
                {
                    number = Request.Form["number"].ToString(),
                    bank = Request.Form["bank"].ToString(),
                    type = Convert.ToInt32(Request.Form["type"].ToString()),
                    value = Convert.ToInt32(Request.Form["value"].ToString()),
                    currency_id = Convert.ToInt32(Request.Form["currency_id"].ToString())
                };
                db.accounts.InsertOnSubmit(acc);
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
                return RedirectToAction("Index", "Accounts");
            }

            List<currency> Currencies = new List<currency>();
            Currencies = db.currencies.ToList();
            ViewData["Currencies"] = Currencies;

            return View();
        }

        public ActionResult EditAccount(int id)
        {
            if (Request.Form["number"] != null)
            {
                var acc = (from account in db.accounts
                           where account.account_id == id
                           select account).SingleOrDefault();

                acc.number = Request.Form["number"].ToString();
                acc.bank = Request.Form["bank"].ToString();
                acc.type = Convert.ToInt32(Request.Form["type"].ToString());
                acc.value = Convert.ToInt32(Request.Form["value"].ToString());
                acc.currency_id = Convert.ToInt32(Request.Form["currency_id"].ToString());

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
                return RedirectToAction("Index", "Accounts");
            }
            else
            {
                var acc = (from account in db.accounts
                           where account.account_id == id
                           select account).SingleOrDefault();
                ViewBag.Account = acc;

                List<currency> Currencies = new List<currency>();
                Currencies = db.currencies.ToList();
                ViewData["Currencies"] = Currencies;
                return View();
            }
        }

        public ActionResult DeleteAccount(int id)
        {
            var acc = (from account in db.accounts
                       where account.account_id == id
                       select account).SingleOrDefault();
            db.accounts.DeleteOnSubmit(acc);

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

            return RedirectToAction("Index", "Accounts");
        }
    }
}