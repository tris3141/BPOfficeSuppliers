using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BPOfficeSupplies.BusinessLogic;
using BPOfficeSupplies.Models;
using PagedList;

namespace BPOfficeSupplies.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        CategoryLogic logic = new CategoryLogic();

        public ActionResult AllCategories(string sortOrder, string currentFilter, string searchString, int? page)
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

            List<Category> Categories = logic.GetCategories();

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                Categories = Categories.Where(p => p.CategoryName.ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Categories = Categories.OrderByDescending(c => c.CategoryName).ToList();
                    break;
                case "Date":
                    Categories = Categories.OrderBy(c => c.DateAdded).ToList();
                    break;
                default:
                    Categories = Categories.OrderBy(c => c.DateAdded).ToList();
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.categories = Categories;
            return View(Categories.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddCategory(string CategoryName)
        {
            var saved = logic.AddCategoryAlternative(CategoryName);
            TempData["AlertMessage"] = saved;
            return RedirectToAction("AllCategories");
        }

        public ActionResult UpdateCategory(string id, string CategoryName)
        {
            var updated = logic.UpdateCategory(id, CategoryName);
            TempData["AlertMessage"] = updated;
            return RedirectToAction("AllCategories");
        }

        [HttpGet]
        public ActionResult GetCategory(string id)
        {
            return Json(logic.GetCategory(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteCategory(string id)
        {
            var deleted = logic.DeleteCategory(id);
            TempData["AlertMessage"] = deleted;
            return RedirectToAction("AllCategories");
        }
    }

   
}
