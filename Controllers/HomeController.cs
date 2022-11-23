using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BPOfficeSupplies.Models;
using BPOfficeSupplies.BusinessLogic;
using PagedList;

namespace BPOfficeSupplies.Controllers
{
    public class HomeController : Controller
    {
        ItemLogic logic = new ItemLogic();
        CategoryLogic categorylogic = new CategoryLogic();
        public ActionResult Index()
        {
            using (var db = new ApplicationDbContext())
            {
                ViewBag.users = db.Users.ToList();
            }

            if(User.Identity.Name=="Admin@gmail.com")
            {
                return RedirectToAction("Admin", "Home");
            }
            else
            {
                return RedirectToAction("Customer", "Home");
            }
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Customer(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;

            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<Item> Items = logic.GetItems();

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                Items = Items.Where(p => p.ItemNumber.ToLower().Contains(search) || p.Description.ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Items = Items.OrderByDescending(c => c.Description).ToList();
                    break;
                case "Date":
                    Items = Items.OrderBy(c => c.DateAdded).ToList();
                    break;
                default:
                    Items = Items.OrderBy(c => c.DateAdded).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.categories = categorylogic.GetCategories();
            return View(Items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Admin(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;

            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<Item> Items = logic.GetItems();

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                Items = Items.Where(p => p.ItemNumber.ToLower().Contains(search) || p.Description.ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Items = Items.OrderByDescending(c => c.Description).ToList();
                    break;
                case "Date":
                    Items = Items.OrderBy(c => c.DateAdded).ToList();
                    break;
                default:
                    Items = Items.OrderBy(c => c.DateAdded).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.categories = categorylogic.GetCategories();
            return View(Items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult Main(string sortOrder, string currentFilter, string searchString, int? page)
        {

            return View();
        }

        public ActionResult Catalogue(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;

            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            List<Item> Items = logic.GetItems();

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                Items = Items.Where(p => p.ItemNumber.ToLower().Contains(search) || p.Description.ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Items = Items.OrderByDescending(c => c.Description).ToList();
                    break;
                case "Date":
                    Items = Items.OrderBy(c => c.DateAdded).ToList();
                    break;
                default:
                    Items = Items.OrderBy(c => c.DateAdded).ToList();
                    break;
            }
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.categories = categorylogic.GetCategories();
            return View(Items.ToPagedList(pageNumber, pageSize));
        }
    }
}