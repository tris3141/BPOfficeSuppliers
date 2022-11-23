using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BPOfficeSupplies.Models;

namespace BPOfficeSupplies.Controllers
{
    public class CheckoutsController : Controller
    {
        // GET: Checkouts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Checkout obj)

        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                db.Checkouts.Add(obj);
                db.SaveChanges();
            }
            return View(obj);
        }
    }
}