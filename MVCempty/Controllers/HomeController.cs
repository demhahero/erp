using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCempty.Models;
namespace MVCempty.Controllers
{
    public class HomeController : Controller
    {
        datalinqDataContext db = new datalinqDataContext();

        // GET: Home
        public ActionResult Index()
        {
            List<product> model = new List<product>();
            model = db.products.ToList();
            return View(model);
        }
    }
}