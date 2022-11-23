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
    public class WishListController : Controller
    {
        WishListLogic logic = new WishListLogic();

        [HttpPost]
        public ActionResult AddToWishlist(string ItemId)
        {
            return Json(logic.AddWishItem(ItemId, User.Identity.Name), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult MyWishList(string sortOrder, string currentFilter, string searchString, int? page)
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

            List<WishItem> Items = logic.GetWishList(User.Identity.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                var search = searchString.ToLower().ToString().Trim();
                Items = Items.Where(p => p.ItemId.ToLower().Contains(search)).ToList();
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Items = Items.OrderByDescending(c => c.ItemId).ToList();
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
            return View(Items.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult RemoveItem(string id)
        {
            var removed = logic.RemoveItem(id);
            return Json(removed, JsonRequestBehavior.AllowGet);
        }
    }
}
